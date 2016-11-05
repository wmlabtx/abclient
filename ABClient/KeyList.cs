namespace ABClient
{
    using System.Text;

    /// <summary>
    /// Класс, содержащий только юзеров.
    /// </summary>
    internal static class KeyList
    {
        private readonly static StringBuilder MSb = new StringBuilder();

        static KeyList()
        {
            MSb.AppendLine("140101|Хранитель Врат|g|131103|-");
            MSb.AppendLine("140101|Сын_Хаоса|g|131201|-");
            MSb.AppendLine("140101|SINED|g|131030|-");            
            MSb.AppendLine("140108|$TOHIN$|g|131107|-");
            MSb.AppendLine("140108|clasavchik|g|131208|-");            
            MSb.AppendLine("140108|~TayFly~|g|131208|-");           
            MSb.AppendLine("140108|-Деман-|g|131208|-");            
            MSb.AppendLine("140109|Истенный маг|g|131209|-");            
            MSb.AppendLine("140112|!!!ШаЛунья!!!|g|131212|-");
            MSb.AppendLine("140114|safari|g|131214|-");            
            MSb.AppendLine("140301|Рыж@я Стерв@|g|131129|-");
            MSb.AppendLine("141023|**Statz**|g|131023|-");
            MSb.AppendLine("141018|~Диверсант~|g|131017|-");
            MSb.AppendLine("141018|Одинокий Лев|g|131017|-");
            MSb.AppendLine("141101|Н Е К Р О М А Н Т|g|131101|-");            
            MSb.AppendLine("141104|~VanyA~|g|131103|-");
            MSb.AppendLine("140119|Voskreswuy|g|131018|kybik7rubik@yandex.ru");                                                        
            MSb.AppendLine("140125|_Мелкий_|g|131225|-");
            MSb.AppendLine("140126|~Д@йвер~|g|131226|-");            
            MSb.AppendLine("140127|DaCho|g|131227|-");            
            MSb.AppendLine("140202|_*Welcome_to_Hell*_|g|131124|Аксон");
            MSb.AppendLine("140222|qwweerty|g|131124|5");
            MSb.AppendLine("140205|Субаровод|g|140105|-");
            MSb.AppendLine("140205|Magic Mystery|g|140105|-");
            MSb.AppendLine("140207|Бобчег|g|140107|-");            
            MSb.AppendLine("140130|_Free Angel_|g|131230|-");
                        
            MSb.AppendLine("140310|Жар_Бог_Шуга|g|131208|-");
            MSb.AppendLine("140210|Safari|g|131208|-");
            MSb.AppendLine("140211|)костас(|g|140111|-");
            MSb.AppendLine("140224|(Непостижимый)|g|131224|-");
            MSb.AppendLine("140217|KAPTOLLIKA|g|131224|-");

            MSb.AppendLine("140307|G@geR|g|140107|-");
            MSb.AppendLine("140520|**царь скорпионов**|g|131220|-");
            MSb.AppendLine("141019|BloodMan1985|g|131019|i_kovtun@inbox.ru");
            MSb.AppendLine("141215|Умник|g|091227|-");
            MSb.AppendLine("141231|Naskez|g|131231|-");
            MSb.AppendLine("141216|Невропатолог|g|131216|-");
            MSb.AppendLine("141216|Воставший из зада|g|131216|-");
            MSb.AppendLine("141225|Чунга чанга|g|131019|-");

            MSb.AppendLine("140610|Гриббо|g|131019|-");
            MSb.AppendLine("140212|borbos|g|140112|-");
            MSb.AppendLine("140216|Blackpool|g|-|-");
            MSb.AppendLine("140219|$_Mr_Rich_$|g|-|-");
            MSb.AppendLine("140227|крызь|g|131227|-");
           
            MSb.AppendLine("140301|Olyanka|g|-|-");            
            MSb.AppendLine("140216|Agent J|g|-|-");
            MSb.AppendLine("140216|Малинка|g|-|-");
            MSb.AppendLine("140501|Mongoose|g|131025|-");
            MSb.AppendLine("140301|Малыша|g|131025|-");
            MSb.AppendLine("140301|tjm_oo|g|131025|-");
            MSb.AppendLine("140301|ПаУчеГГ-)|g|131027|-");
            MSb.AppendLine("140301|Nefritiya|g|131027|-");
            MSb.AppendLine("140301|Sch0kK|g|131227|-");
            MSb.AppendLine("140301|Xsurround|g|131227|-");
            MSb.AppendLine("140301|-skay-|g|131227|-");
            MSb.AppendLine("140302|Полосатый Кот|g|131210|-");
            MSb.AppendLine("141202|***Mikki***|g|131226|-");
            MSb.AppendLine("140302|komIIot_Pskov|g|131223|-");
            MSb.AppendLine("140302|Просто-чеЛ|g|131206|-");
            MSb.AppendLine("140303|*Бастард*|g|131206|-");
            MSb.AppendLine("140303|*Трогвар*|g|131206|-");
            MSb.AppendLine("140305|Развратник|g|131020|aspen1989@inbox.ru");
            MSb.AppendLine("140306|~_RAMA~_|g|131020|-");
            MSb.AppendLine("140307|Прост@_Князь|g|140107|-");
            MSb.AppendLine("140307|Храброе Сердце|g|140107|-");
            MSb.AppendLine("140308|fr0zjk3^^|g|131107|-");
            MSb.AppendLine("140308|running in darkness|g|131107|-");
            MSb.AppendLine("140308|Судья Аркадии|g|131208|-");
            MSb.AppendLine("140308|Aspire|g|131208|-");
            MSb.AppendLine("140409|Ахилес сын Пилея|g|131023|-");
            MSb.AppendLine("140310|Белый Лебедь|g|140108|-");
            MSb.AppendLine("140310|***OKHO_B_DBEPb***|g|131207|-");
            MSb.AppendLine("140310|~Сладенький~|g|131207|-");
            MSb.AppendLine("140312|shadow_warrior|g|-|-");
            MSb.AppendLine("140412|Ironiya|g|131029|-");         
            MSb.AppendLine("140410|-=Stells=-|g|131208|-");           
            MSb.AppendLine("140410|Асамуель|g|131204|-");
            MSb.AppendLine("140410|Лесничий|g|131208|-");            
            MSb.AppendLine("140310|London Eye|g|131208|-");
            MSb.AppendLine("140310|sergei6|g|131208|-");
            MSb.AppendLine("140601|_St@uff_|g|131029|-");
            MSb.AppendLine("140314|весельчак 25|g|131208|-");
            MSb.AppendLine("140314|~Норик~|g|131208|-");
            MSb.AppendLine("140315|KOMRAT|g|131208|-");
            MSb.AppendLine("140409|-MC Sp1DeR-|g|131023|-");

            MSb.AppendLine("140501|Аксон|g|131027|-");
            MSb.AppendLine("140317|Карамболь|g|131201|-");

            MSb.AppendLine("140312|~Геката~|g|131208|-");
            MSb.AppendLine("140812|demned@|g|131218|-");
            MSb.AppendLine("140812|Тёмный Егерь|g|131218|-");          

            MSb.AppendLine("140319|PaKeMoN|g|131208|-");

            MSb.AppendLine("140410|Gr3MLiN|g|131206|-");

            MSb.AppendLine("150101|райская птица|g|090101|-");
            MSb.AppendLine("150101|Coolerello|g|090101|-");
            MSb.AppendLine("150101|Черный|g|090101|-");
            MSb.AppendLine("150110|AnechkaBlondy|g|140110|-");
            MSb.AppendLine("150114|Muhamed_mc|g|140114|-");            
            MSb.AppendLine("150110|The Best Gladiator|g|140110|-");

            MSb.AppendLine("140321|angel of darknes|g|-|-");
            MSb.AppendLine("140527|-Exit-|g|131020|beltsov89@mail.ru");
            MSb.AppendLine("140323|Why_me|g|131218|-");
            MSb.AppendLine("140415|CTPAX|g|131208|-");
            MSb.AppendLine("140526|~~0тшельник~~|g|131112|-");
            MSb.AppendLine("140526|Nana|g|131112|-");
            MSb.AppendLine("140327|Gargonit|g|131019|-");

            MSb.AppendLine("140627|~Ловкачка~|g|131019|-");
            MSb.AppendLine("140603|челенджер|g|131020|aspen1989@inbox.ru");
            MSb.AppendLine("140617|natalimalfoy|g|131217|-");            
            MSb.AppendLine("140404|Мегамаг)|g|140113|-");
            MSb.AppendLine("140404|лунный кот|g|131020|-");
            MSb.AppendLine("140405|Титикака|g|131020|-");
            MSb.AppendLine("140505|Ужас2006|g|131020|aspen1989@inbox.ru");
            MSb.AppendLine("140505|Malibu|g|131020|-");
            MSb.AppendLine("140406|Спартач|g|131206|-");
            MSb.AppendLine("140406|-styleee-|g|131223|-");
            MSb.AppendLine("140406|перевозщик|g|131223|-");
            MSb.AppendLine("140409|шурик_рулит|g|131023|-");
            MSb.AppendLine("140410|zSweng|g|131023|-");
            MSb.AppendLine("140410|Dark-wolf|g|131208|-");
            MSb.AppendLine("140410|last_resort|g|140111|-");
            MSb.AppendLine("140411|-герой-|g|140111|-");
            MSb.AppendLine("140411|VV|g|140111|-");
            MSb.AppendLine("140413|убийца с топором|g|131208|-");
            MSb.AppendLine("140415|м@кл@уд|g|131208|-");
            MSb.AppendLine("140517|~life is lie~|g|131208|-");
            MSb.AppendLine("140417|Astet|g|131208|-");
            MSb.AppendLine("140420|TeZon|g|-|-");
            MSb.AppendLine("140520|*ChEv ChELiOs*|g|131025|-");
            MSb.AppendLine("140420|к0т|g|131023|-");
            MSb.AppendLine("140922|Call of Duty|g|131023|-");
            MSb.AppendLine("140423|Гордый Мышёнок|g|131023|-");
            MSb.AppendLine("140424|jjoojjoo|g|131218|-");
            MSb.AppendLine("140424|rash_92|g|-|-");
            MSb.AppendLine("140424|Не Буди|g|-|-");
            MSb.AppendLine("140425|Voin|g|131112|-");
            MSb.AppendLine("140425|Коршун птица боевая|g|131106|-");
            MSb.AppendLine("140426|SPAMER|g|140326|SPAMER 5DNV погашено");
            MSb.AppendLine("140427|Тень вершителя|g|131106|-");
            MSb.AppendLine("140430|амено|g|131019|-");
            MSb.AppendLine("140530|~AcR1D~|g|131019|-");
            MSb.AppendLine("140530|Ангел Ужаса|g|131019|-");
            MSb.AppendLine("140701|Fermio|g|131029|-");
            MSb.AppendLine("140501|armagidon_bog|g|131029|-");
            MSb.AppendLine("140501|У_б_и_й_ц_А|g|131019|-");
            MSb.AppendLine("140505|Em|g|131020|-");
            MSb.AppendLine("140501|GoD Killer|g|131020|-");
            MSb.AppendLine("140501|dsaaan|g|131020|-");
            MSb.AppendLine("140501|TenZo-R|g|131019|-");
            MSb.AppendLine("140501|twister|g|131020|-");
            MSb.AppendLine("140502|The Perfect Killer|g|131020|-");
            MSb.AppendLine("140702|Африканский Чукча|g|131020|-");
            MSb.AppendLine("140504|Vutalui|g|131209|-");
            MSb.AppendLine("140503|$вишенка$|g|131029|-");
            MSb.AppendLine("140504|-ARMAN-|g|131029|-");
            MSb.AppendLine("140507|Легенда 13|g|131020|-");
            MSb.AppendLine("140508|Blade runner|g|131107|-");
            MSb.AppendLine("140510|~ Diamond ThoRn ~|g|131208|-");
            MSb.AppendLine("140510|Жена-Зл0|g|131208|-");
            MSb.AppendLine("140608|Kapam|g|131107|-");
            MSb.AppendLine("140508|ULTRAS|g|131029|-");
            MSb.AppendLine("140509|Cute_Devil|g|131107|-");
            MSb.AppendLine("140710|Zver In Me|g|131107|-");
            MSb.AppendLine("140510|Gaffer|g|131107|-");
            MSb.AppendLine("140510|*AgentMorga*|g|131208|-");
            MSb.AppendLine("140510|Боженька|g|131208|-");
            MSb.AppendLine("140510|Diabel|g|131208|-");
            MSb.AppendLine("140510|Stealth Assassin|g|131208|-");
            MSb.AppendLine("140510|Isoclene|g|131208|-");
            MSb.AppendLine("140510|Папуля|g|131023|-");
            MSb.AppendLine("140612|translot|g|131125|-");
            MSb.AppendLine("140511|KarMenta|g|131125|-");
            MSb.AppendLine("140512|Andy|g|140107|-");
            MSb.AppendLine("140518|НовыЙ Мутный|g|131208|-");
            MSb.AppendLine("140601|_Орлиный коготь_|g|131018|долг с 0408");
            MSb.AppendLine("140516|Уткин|g|131208|-");
            MSb.AppendLine("140513|НеПоБрИтЫй|g|131027|-");
            MSb.AppendLine("140514|machine man|g|131027|-");
            MSb.AppendLine("140515|stneider|g|131027|-");
            MSb.AppendLine("140515|Richardson|g|131020|-");
            MSb.AppendLine("140517|ТВЁРДЫЙ ШАНКР|g|140107|-");
            MSb.AppendLine("140518|shabanize-88|g|140107|-");
            MSb.AppendLine("140519|Мертвец|g|140107|-");
            MSb.AppendLine("140627|DangerMAGister|g|131019|-");
            MSb.AppendLine("140622|Бананза|g|-|-");
            MSb.AppendLine("140523|Man@war|g|-|-");
            MSb.AppendLine("140524|Изгнаник из Рая|g|-|-");
            MSb.AppendLine("140527|*Soldier of darkness|g|-|-");
            MSb.AppendLine("140524|Леший|g|-|-");
            MSb.AppendLine("140524|lLlife|g|-|-");
            MSb.AppendLine("140524|vadikvoin|g|-|-");
            MSb.AppendLine("140524|LeraRen|g|-|-");
            MSb.AppendLine("140527|Dante Alighieri|g|131019|-");
            MSb.AppendLine("140526|Tuborg|g|131106|-");
            MSb.AppendLine("140526|Xameleon|g|131106|-");
            MSb.AppendLine("140606|Забытый во Времени|g|131020|-");
            MSb.AppendLine("140525|Некий Он|g|-|-");
            MSb.AppendLine("140525|Killer_29|g|-|-");
            MSb.AppendLine("140525|Самка_БоевогоКролика|g|-|-");
            MSb.AppendLine("140526|(~ХИЩНИК~)|g|-|-");
            MSb.AppendLine("140526|agent1984_|g|-|-");
            MSb.AppendLine("140602|KIL|g|131220|-");
        }

        /*
Просто-чеЛ, _Орлиный коготь_, ТВЁРДЫЙ ШАНКР, _*Welcome_to_Hell*_, Аксон, Малыша, ~TORTIK~, agent1984_
         */ 


        internal static string Users
        {
            get
            {
                return MSb.ToString();
            }
        }
    }
}
