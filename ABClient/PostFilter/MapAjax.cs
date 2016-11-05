namespace ABClient.PostFilter
{
    using System;
    using System.Globalization;
    using ABForms;
    using ExtMap;

    internal static partial class Filter
    {
        private static string MapAjax(string html)
        {
            const string patternVarMap = "var map = [[";
            var posVarMap = html.IndexOf(patternVarMap, StringComparison.Ordinal);
            if (posVarMap == -1)
                return html;

            posVarMap += patternVarMap.Length;
            var posComma = html.IndexOf(',', posVarMap);
            if (posComma == -1)
                return html;

            var stringOurLocationX = html.Substring(posVarMap, posComma - posVarMap);
            posComma++;
            var posNextComma = html.IndexOf(',', posComma);
            if (posNextComma == -1)
                return html;

            var stringOurLocationY = html.Substring(posComma, posNextComma - posComma);
            var positionOurLocation = string.Format(
                CultureInfo.InvariantCulture, 
                "{0}/{1}_{2}", 
                stringOurLocationY, 
                stringOurLocationX, 
                stringOurLocationY);
            if (Map.Location.ContainsKey(positionOurLocation))
            {
                var ourLocation = Map.Location[positionOurLocation];
                var regNum = ourLocation.RegNum;
                AppVars.Profile.MapLocation = regNum;
                try
                {
                    AppVars.MainForm.UpdateLocationSafe(regNum);
                }
                catch
                {
                }
            }

            posComma = posNextComma + 1;
            posNextComma = html.IndexOf(',', posComma);
            if (posNextComma == -1)
            {
                return html;
            }

            var movingTime = html.Substring(posComma, posNextComma - posComma);
            if (!string.IsNullOrEmpty(movingTime))
            {
                AppVars.MovingTime = movingTime;
            }

            Map.MovableCells.Clear();
            const string patternDoubleBrackets = ",[[";
            var posOpenBrackets = html.IndexOf(patternDoubleBrackets, posVarMap, StringComparison.Ordinal);
            if (posOpenBrackets == -1)
            {
                return html;
            }

            var posOpenBracket = posOpenBrackets + patternDoubleBrackets.Length;
            while (posOpenBracket != -1)
            {
                var posCloseBracket = html.IndexOf(']', posOpenBracket);
                var insideBrackets = html.Substring(posOpenBracket, posCloseBracket - posOpenBracket);
                if (insideBrackets.IndexOf(';') != -1)
                {
                    return html;    
                }

                var parsInsideBrackets = insideBrackets.Split(',');
                if (parsInsideBrackets.Length == 3)
                {
                    var stringCoordX = parsInsideBrackets[0];
                    var stringCoordY = parsInsideBrackets[1];
                    var position = string.Format(CultureInfo.InvariantCulture, "{0}/{1}_{2}", stringCoordY, stringCoordX, stringCoordY);
                    var stringMagicCode = parsInsideBrackets[2].Trim('"');
                    Map.MovableCells.Add(position, stringMagicCode);

                    Position ppp;
                    if (Map.Location.TryGetValue(position, out ppp))
                    {
                        Cell ccc;
                        if (!Map.Cells.TryGetValue(ppp.RegNum, out ccc))
                        {
                            try
                            {
                                if (AppVars.MainForm != null)
                                {
                                    AppVars.MainForm.BeginInvoke(
                                        new UpdateWriteChatMsgDelegate(AppVars.MainForm.WriteChatMsg),
                                        $"Отсутствует клетка {ppp.RegNum}");
                                }
                            }
                            catch (InvalidOperationException)
                            {
                            }
                        }
                    }
                }

                posOpenBracket = html.IndexOf('[', posCloseBracket);
                if (posOpenBracket == -1)
                {
                    break;
                }

                posOpenBracket++;
            }

            if (AppVars.AutoMoving)
            {
                if (AppVars.Profile.MapLocation.Equals(AppVars.AutoMovingDestinaton))
                {
                    if (!AppVars.DoSearchBox)
                    {
                        try
                        {
                            if (AppVars.MainForm != null)
                            {
                                AppVars.MainForm.BeginInvoke(
                                    new NavigatorOffInvokeDelegate(AppVars.MainForm.NavigatorOffInvoke),
                                    new object[] {});
                            }
                        }
                        catch (InvalidOperationException)
                        {
                        }

                        return html;
                    }

                    var destbox = FormMain.FindNextDestForBox();
                    if (string.IsNullOrEmpty(destbox) || (AppVars.MainForm == null))
                    {
                        return html;
                    }

                    AppVars.AutoMovingDestinaton = destbox;
                }

                if (AppVars.AutoMovingMapPath == null || !AppVars.AutoMovingMapPath.CanUseExistingPath(AppVars.Profile.MapLocation, AppVars.AutoMovingDestinaton))
                {
                    var dest = new[] { AppVars.AutoMovingDestinaton };
                    AppVars.AutoMovingMapPath = new MapPath(AppVars.Profile.MapLocation, dest);
                }

                AppVars.AutoMovingNextJump = AppVars.AutoMovingMapPath.NextJump;
                AppVars.AutoMovingJumps = AppVars.AutoMovingMapPath.Jumps;
                AppVars.AutoMovingCityGate = AppVars.AutoMovingMapPath.CityGate;

                if (AppVars.AutoMovingMapPath.IsNextTeleport)
                {
                    var newhtml = MainPhpFindEnter(html);
                    if (!string.IsNullOrEmpty(newhtml))
                    {
                        return newhtml;
                    }
                }
                else
                {
                    if (AppVars.AutoMovingMapPath.IsNextCity)
                    {
                        var newhtml = MainPhpFindEnter(html);
                        if (!string.IsNullOrEmpty(newhtml))
                        {
                            return newhtml;
                        }
                    }
                    else
                    {
                        if (AppVars.Profile.ShowTrayBaloons)
                        {
                            try
                            {
                                var message = string.Format(CultureInfo.InvariantCulture, "Перемещаемся в {0} (еще {1})",
                                    AppVars.AutoMovingMapPath.NextJump, AppVars.AutoMovingMapPath.Jumps);
                                if (AppVars.MainForm != null) AppVars.MainForm.UpdateTrayBaloonSafe(message);
                            }
                            catch
                            {
                            }
                        }

                        var coorn = Map.InvLocation[AppVars.AutoMovingMapPath.NextJump];
                        var position = Map.Location[coorn];
                        var callMove = string.Format(CultureInfo.InvariantCulture, "moveMapTo({0}, {1}, map[0][2]);",
                            position.X, position.Y);
                        const string patternViewMap = "view_map();";
                        var poscript = html.IndexOf(patternViewMap, StringComparison.OrdinalIgnoreCase);
                        if (poscript != -1)
                        {
                            poscript += patternViewMap.Length;
                            html = html.Insert(poscript, callMove);
                        }
                    }
                }
            }

            return html;
        }
    }
}