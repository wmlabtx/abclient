using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ABClient.ABForms;
using ABClient.ExtMap;
using ABClient.PostFilter;

// Token: 0x02000098 RID: 152
internal static class Class72
{
	// Token: 0x0600052E RID: 1326 RVA: 0x00037A20 File Offset: 0x00035C20
	static Class72()
	{
		Class72.smethod_95(new Dictionary<string, string>());
		Class72.smethod_101(string.Empty);
		Class72.smethod_103(string.Empty);
		Class72.smethod_147(new int[4]);
		Class72.dateTime_10 = DateTime.MinValue;
		Class72.dateTime_11 = DateTime.Now.AddMinutes(5.0);
	}

	// Token: 0x0600052F RID: 1327 RVA: 0x0000777A File Offset: 0x0000597A
	internal static ClearExplorerCacheForm smethod_0()
	{
		return Class72.clearExplorerCacheForm_0;
	}

	// Token: 0x06000530 RID: 1328 RVA: 0x00007781 File Offset: 0x00005981
	internal static void smethod_1(ClearExplorerCacheForm clearExplorerCacheForm_1)
	{
		Class72.clearExplorerCacheForm_0 = clearExplorerCacheForm_1;
	}

	// Token: 0x06000531 RID: 1329 RVA: 0x00007789 File Offset: 0x00005989
	internal static bool smethod_2()
	{
		return Class72.bool_0;
	}

	// Token: 0x06000532 RID: 1330 RVA: 0x00007790 File Offset: 0x00005990
	internal static void smethod_3(bool bool_54)
	{
		Class72.bool_0 = bool_54;
	}

	// Token: 0x06000533 RID: 1331 RVA: 0x00007798 File Offset: 0x00005998
	internal static DateTime smethod_4()
	{
		return Class72.dateTime_1;
	}

	// Token: 0x06000534 RID: 1332 RVA: 0x0000779F File Offset: 0x0000599F
	internal static void smethod_5(DateTime dateTime_14)
	{
		Class72.dateTime_1 = dateTime_14;
	}

	// Token: 0x06000535 RID: 1333 RVA: 0x000077A7 File Offset: 0x000059A7
	internal static string smethod_6()
	{
		return Class72.string_5;
	}

	// Token: 0x06000536 RID: 1334 RVA: 0x000077AE File Offset: 0x000059AE
	internal static void smethod_7(string string_56)
	{
		Class72.string_5 = string_56;
	}

	// Token: 0x06000537 RID: 1335 RVA: 0x000077B6 File Offset: 0x000059B6
	internal static bool smethod_8()
	{
		return Class72.bool_1;
	}

	// Token: 0x06000538 RID: 1336 RVA: 0x000077BD File Offset: 0x000059BD
	internal static void smethod_9(bool bool_54)
	{
		Class72.bool_1 = bool_54;
	}

	// Token: 0x06000539 RID: 1337 RVA: 0x000077C5 File Offset: 0x000059C5
	internal static bool smethod_10()
	{
		return Class72.bool_2;
	}

	// Token: 0x0600053A RID: 1338 RVA: 0x000077CC File Offset: 0x000059CC
	internal static void smethod_11(bool bool_54)
	{
		Class72.bool_2 = bool_54;
	}

	// Token: 0x0600053B RID: 1339 RVA: 0x000077D4 File Offset: 0x000059D4
	internal static bool smethod_12()
	{
		return Class72.bool_3;
	}

	// Token: 0x0600053C RID: 1340 RVA: 0x000077DB File Offset: 0x000059DB
	internal static void smethod_13(bool bool_54)
	{
		Class72.bool_3 = bool_54;
	}

	// Token: 0x0600053D RID: 1341 RVA: 0x000077E3 File Offset: 0x000059E3
	internal static DateTime smethod_14()
	{
		return Class72.dateTime_2;
	}

	// Token: 0x0600053E RID: 1342 RVA: 0x000077EA File Offset: 0x000059EA
	internal static void smethod_15(DateTime dateTime_14)
	{
		Class72.dateTime_2 = dateTime_14;
	}

	// Token: 0x0600053F RID: 1343 RVA: 0x000077F2 File Offset: 0x000059F2
	internal static string smethod_16()
	{
		return Class72.string_6;
	}

	// Token: 0x06000540 RID: 1344 RVA: 0x000077F9 File Offset: 0x000059F9
	internal static void smethod_17(string string_56)
	{
		Class72.string_6 = string_56;
	}

	/*// Token: 0x06000541 RID: 1345 RVA: 0x00007801 File Offset: 0x00005A01
	internal static Enum4 smethod_18()
	{
		return Class72.enum4_0;
	}

	// Token: 0x06000542 RID: 1346 RVA: 0x00007808 File Offset: 0x00005A08
	internal static void smethod_19(Enum4 enum4_1)
	{
		Class72.enum4_0 = enum4_1;
	}*/

	// Token: 0x06000543 RID: 1347 RVA: 0x00007810 File Offset: 0x00005A10
	internal static string smethod_20()
	{
		return Class72.string_7;
	}

	// Token: 0x06000544 RID: 1348 RVA: 0x00007817 File Offset: 0x00005A17
	internal static void smethod_21(string string_56)
	{
		Class72.string_7 = string_56;
	}

	// Token: 0x06000545 RID: 1349 RVA: 0x0000781F File Offset: 0x00005A1F
	internal static string smethod_22()
	{
		return Class72.string_8;
	}

	// Token: 0x06000546 RID: 1350 RVA: 0x00007826 File Offset: 0x00005A26
	internal static void smethod_23(string string_56)
	{
		Class72.string_8 = string_56;
	}

	// Token: 0x06000547 RID: 1351 RVA: 0x0000782E File Offset: 0x00005A2E
	internal static byte[] smethod_24()
	{
		return Class72.byte_0;
	}

	// Token: 0x06000548 RID: 1352 RVA: 0x00007835 File Offset: 0x00005A35
	internal static void smethod_25(byte[] byte_1)
	{
		Class72.byte_0 = byte_1;
	}

	// Token: 0x06000549 RID: 1353 RVA: 0x0000783D File Offset: 0x00005A3D
	internal static int smethod_26()
	{
		return Class72.int_0;
	}

	// Token: 0x0600054A RID: 1354 RVA: 0x00007844 File Offset: 0x00005A44
	internal static void smethod_27(int int_19)
	{
		Class72.int_0 = int_19;
	}

	// Token: 0x0600054B RID: 1355 RVA: 0x0000784C File Offset: 0x00005A4C
	internal static DateTime smethod_28()
	{
		return Class72.dateTime_3;
	}

	// Token: 0x0600054C RID: 1356 RVA: 0x00007853 File Offset: 0x00005A53
	internal static void smethod_29(DateTime dateTime_14)
	{
		Class72.dateTime_3 = dateTime_14;
	}

	// Token: 0x0600054D RID: 1357 RVA: 0x0000785B File Offset: 0x00005A5B
	internal static bool smethod_30()
	{
		return Class72.bool_4;
	}

	// Token: 0x0600054E RID: 1358 RVA: 0x00007862 File Offset: 0x00005A62
	internal static void smethod_31(bool bool_54)
	{
		Class72.bool_4 = bool_54;
	}

	// Token: 0x0600054F RID: 1359 RVA: 0x0000786A File Offset: 0x00005A6A
	internal static bool smethod_32()
	{
		return Class72.bool_5;
	}

	// Token: 0x06000550 RID: 1360 RVA: 0x00007871 File Offset: 0x00005A71
	internal static void smethod_33(bool bool_54)
	{
		Class72.bool_5 = bool_54;
	}

	// Token: 0x06000551 RID: 1361 RVA: 0x00007879 File Offset: 0x00005A79
	internal static bool smethod_34()
	{
		return Class72.bool_6;
	}

	// Token: 0x06000552 RID: 1362 RVA: 0x00007880 File Offset: 0x00005A80
	internal static void smethod_35(bool bool_54)
	{
		Class72.bool_6 = bool_54;
	}

	// Token: 0x06000553 RID: 1363 RVA: 0x00007888 File Offset: 0x00005A88
	internal static bool smethod_36()
	{
		return Class72.bool_7;
	}

	// Token: 0x06000554 RID: 1364 RVA: 0x0000788F File Offset: 0x00005A8F
	internal static void smethod_37(bool bool_54)
	{
		Class72.bool_7 = bool_54;
	}

	// Token: 0x06000555 RID: 1365 RVA: 0x00007897 File Offset: 0x00005A97
	internal static string smethod_38()
	{
		return Class72.string_9;
	}

	// Token: 0x06000556 RID: 1366 RVA: 0x0000789E File Offset: 0x00005A9E
	internal static void smethod_39(string string_56)
	{
		Class72.string_9 = string_56;
	}

	// Token: 0x06000557 RID: 1367 RVA: 0x000078A6 File Offset: 0x00005AA6
	internal static string smethod_40()
	{
		return Class72.string_10;
	}

	// Token: 0x06000558 RID: 1368 RVA: 0x000078AD File Offset: 0x00005AAD
	internal static void smethod_41(string string_56)
	{
		Class72.string_10 = string_56;
	}

	// Token: 0x06000559 RID: 1369 RVA: 0x000078B5 File Offset: 0x00005AB5
	internal static int smethod_42()
	{
		return Class72.int_1;
	}

	// Token: 0x0600055A RID: 1370 RVA: 0x000078BC File Offset: 0x00005ABC
	internal static void smethod_43(int int_19)
	{
		Class72.int_1 = int_19;
	}

	// Token: 0x0600055B RID: 1371 RVA: 0x000078C4 File Offset: 0x00005AC4
	internal static CityGateType smethod_44()
	{
		return Class72.cityGateType_0;
	}

	// Token: 0x0600055C RID: 1372 RVA: 0x000078CB File Offset: 0x00005ACB
	internal static void smethod_45(CityGateType cityGateType_1)
	{
		Class72.cityGateType_0 = cityGateType_1;
	}

	// Token: 0x0600055D RID: 1373 RVA: 0x000078D3 File Offset: 0x00005AD3
	internal static MapPath smethod_46()
	{
		return Class72.mapPath_0;
	}

	// Token: 0x0600055E RID: 1374 RVA: 0x000078DA File Offset: 0x00005ADA
	internal static void smethod_47(MapPath mapPath_1)
	{
		Class72.mapPath_0 = mapPath_1;
	}

	// Token: 0x0600055F RID: 1375 RVA: 0x000078E2 File Offset: 0x00005AE2
	internal static string smethod_48()
	{
		return Class72.string_11;
	}

	// Token: 0x06000560 RID: 1376 RVA: 0x000078E9 File Offset: 0x00005AE9
	internal static void smethod_49(string string_56)
	{
		Class72.string_11 = string_56;
	}

	// Token: 0x06000561 RID: 1377 RVA: 0x000078F1 File Offset: 0x00005AF1
	internal static bool smethod_50()
	{
		return Class72.bool_8;
	}

	// Token: 0x06000562 RID: 1378 RVA: 0x000078F8 File Offset: 0x00005AF8
	internal static void smethod_51(bool bool_54)
	{
		Class72.bool_8 = bool_54;
	}

	// Token: 0x06000563 RID: 1379 RVA: 0x00007900 File Offset: 0x00005B00
	internal static int smethod_52()
	{
		return Class72.int_2;
	}

	// Token: 0x06000564 RID: 1380 RVA: 0x00007907 File Offset: 0x00005B07
	internal static void smethod_53(int int_19)
	{
		Class72.int_2 = int_19;
	}

	// Token: 0x06000565 RID: 1381 RVA: 0x0000790F File Offset: 0x00005B0F
	internal static bool smethod_54()
	{
		return Class72.bool_9;
	}

	// Token: 0x06000566 RID: 1382 RVA: 0x00007916 File Offset: 0x00005B16
	internal static void smethod_55(bool bool_54)
	{
		Class72.bool_9 = bool_54;
	}

	// Token: 0x06000567 RID: 1383 RVA: 0x0000791E File Offset: 0x00005B1E
	internal static bool smethod_56()
	{
		return Class72.bool_10;
	}

	// Token: 0x06000568 RID: 1384 RVA: 0x00007925 File Offset: 0x00005B25
	internal static void smethod_57(bool bool_54)
	{
		Class72.bool_10 = bool_54;
	}

	// Token: 0x06000569 RID: 1385 RVA: 0x0000792D File Offset: 0x00005B2D
	internal static DateTime smethod_58()
	{
		return Class72.dateTime_4;
	}

	// Token: 0x0600056A RID: 1386 RVA: 0x00007934 File Offset: 0x00005B34
	internal static void smethod_59(DateTime dateTime_14)
	{
		Class72.dateTime_4 = dateTime_14;
	}

	// Token: 0x0600056B RID: 1387 RVA: 0x0000793C File Offset: 0x00005B3C
	internal static bool smethod_60()
	{
		return Class72.bool_11;
	}

	// Token: 0x0600056C RID: 1388 RVA: 0x00007943 File Offset: 0x00005B43
	internal static void smethod_61(bool bool_54)
	{
		Class72.bool_11 = bool_54;
	}

	// Token: 0x0600056D RID: 1389 RVA: 0x0000794B File Offset: 0x00005B4B
	internal static bool smethod_62()
	{
		return Class72.bool_12;
	}

	// Token: 0x0600056E RID: 1390 RVA: 0x00007952 File Offset: 0x00005B52
	internal static void smethod_63(bool bool_54)
	{
		Class72.bool_12 = bool_54;
	}

	// Token: 0x0600056F RID: 1391 RVA: 0x0000795A File Offset: 0x00005B5A
	internal static bool smethod_64()
	{
		return Class72.bool_13;
	}

	// Token: 0x06000570 RID: 1392 RVA: 0x00007961 File Offset: 0x00005B61
	internal static void smethod_65(bool bool_54)
	{
		Class72.bool_13 = bool_54;
	}

	// Token: 0x06000571 RID: 1393 RVA: 0x00007969 File Offset: 0x00005B69
	internal static bool smethod_66()
	{
		return Class72.bool_14;
	}

	// Token: 0x06000572 RID: 1394 RVA: 0x00007970 File Offset: 0x00005B70
	internal static void smethod_67(bool bool_54)
	{
		Class72.bool_14 = bool_54;
	}

	// Token: 0x06000573 RID: 1395 RVA: 0x00007978 File Offset: 0x00005B78
	internal static string smethod_68()
	{
		return Class72.string_12;
	}

	// Token: 0x06000574 RID: 1396 RVA: 0x0000797F File Offset: 0x00005B7F
	internal static void smethod_69(string string_56)
	{
		Class72.string_12 = string_56;
	}

	// Token: 0x06000575 RID: 1397 RVA: 0x00007987 File Offset: 0x00005B87
	internal static string smethod_70()
	{
		return Class72.string_13;
	}

	// Token: 0x06000576 RID: 1398 RVA: 0x0000798E File Offset: 0x00005B8E
	internal static void smethod_71(string string_56)
	{
		Class72.string_13 = string_56;
	}

	// Token: 0x06000577 RID: 1399 RVA: 0x00007996 File Offset: 0x00005B96
	internal static string smethod_72()
	{
		return Class72.string_14;
	}

	// Token: 0x06000578 RID: 1400 RVA: 0x0000799D File Offset: 0x00005B9D
	internal static void smethod_73(string string_56)
	{
		Class72.string_14 = string_56;
	}

	// Token: 0x06000579 RID: 1401 RVA: 0x000079A5 File Offset: 0x00005BA5
	internal static string smethod_74()
	{
		return Class72.string_15;
	}

	// Token: 0x0600057A RID: 1402 RVA: 0x000079AC File Offset: 0x00005BAC
	internal static void smethod_75(string string_56)
	{
		Class72.string_15 = string_56;
	}

	// Token: 0x0600057B RID: 1403 RVA: 0x000079B4 File Offset: 0x00005BB4
	internal static string smethod_76()
	{
		return Class72.string_16;
	}

	// Token: 0x0600057C RID: 1404 RVA: 0x000079BB File Offset: 0x00005BBB
	internal static void smethod_77(string string_56)
	{
		Class72.string_16 = string_56;
	}

	// Token: 0x0600057D RID: 1405 RVA: 0x000079C3 File Offset: 0x00005BC3
	internal static string smethod_78()
	{
		return Class72.string_17;
	}

	// Token: 0x0600057E RID: 1406 RVA: 0x000079CA File Offset: 0x00005BCA
	internal static void smethod_79(string string_56)
	{
		Class72.string_17 = string_56;
	}

	// Token: 0x0600057F RID: 1407 RVA: 0x000079D2 File Offset: 0x00005BD2
	internal static string smethod_80()
	{
		return Class72.string_18;
	}

	// Token: 0x06000580 RID: 1408 RVA: 0x000079D9 File Offset: 0x00005BD9
	internal static void smethod_81(string string_56)
	{
		Class72.string_18 = string_56;
	}

	// Token: 0x06000581 RID: 1409 RVA: 0x000079E1 File Offset: 0x00005BE1
	internal static double smethod_82()
	{
		return Class72.double_0;
	}

	// Token: 0x06000582 RID: 1410 RVA: 0x000079E8 File Offset: 0x00005BE8
	internal static void smethod_83(double double_1)
	{
		Class72.double_0 = double_1;
	}

	// Token: 0x06000583 RID: 1411 RVA: 0x000079F0 File Offset: 0x00005BF0
	internal static bool smethod_84()
	{
		return Class72.bool_15;
	}

	// Token: 0x06000584 RID: 1412 RVA: 0x000079F7 File Offset: 0x00005BF7
	internal static void smethod_85(bool bool_54)
	{
		Class72.bool_15 = bool_54;
	}

	// Token: 0x06000585 RID: 1413 RVA: 0x000079FF File Offset: 0x00005BFF
	internal static bool smethod_86()
	{
		return Class72.bool_16;
	}

	// Token: 0x06000586 RID: 1414 RVA: 0x00007A06 File Offset: 0x00005C06
	internal static void smethod_87(bool bool_54)
	{
		Class72.bool_16 = bool_54;
	}

	// Token: 0x06000587 RID: 1415 RVA: 0x00007A0E File Offset: 0x00005C0E
	internal static bool smethod_88()
	{
		return Class72.bool_17;
	}

	// Token: 0x06000588 RID: 1416 RVA: 0x00007A15 File Offset: 0x00005C15
	internal static void smethod_89(bool bool_54)
	{
		Class72.bool_17 = bool_54;
	}

	// Token: 0x06000589 RID: 1417 RVA: 0x00007A1D File Offset: 0x00005C1D
	internal static string smethod_90()
	{
		return Class72.string_19;
	}

	// Token: 0x0600058A RID: 1418 RVA: 0x00007A24 File Offset: 0x00005C24
	internal static void smethod_91(string string_56)
	{
		Class72.string_19 = string_56;
	}

	// Token: 0x0600058B RID: 1419 RVA: 0x00007A2C File Offset: 0x00005C2C
	internal static string smethod_92()
	{
		return Class72.string_20;
	}

	// Token: 0x0600058C RID: 1420 RVA: 0x00007A33 File Offset: 0x00005C33
	internal static void smethod_93(string string_56)
	{
		Class72.string_20 = string_56;
	}

	// Token: 0x0600058D RID: 1421 RVA: 0x00007A3B File Offset: 0x00005C3B
	internal static Dictionary<string, string> smethod_94()
	{
		return Class72.dictionary_1;
	}

	// Token: 0x0600058E RID: 1422 RVA: 0x00007A42 File Offset: 0x00005C42
	internal static void smethod_95(Dictionary<string, string> dictionary_5)
	{
		Class72.dictionary_1 = dictionary_5;
	}

	// Token: 0x0600058F RID: 1423 RVA: 0x00007A4A File Offset: 0x00005C4A
	internal static int smethod_96()
	{
		return Class72.int_3;
	}

	// Token: 0x06000590 RID: 1424 RVA: 0x00007A51 File Offset: 0x00005C51
	internal static void smethod_97(int int_19)
	{
		Class72.int_3 = int_19;
	}

	// Token: 0x06000591 RID: 1425 RVA: 0x00007A59 File Offset: 0x00005C59
	internal static int smethod_98()
	{
		return Class72.int_4;
	}

	// Token: 0x06000592 RID: 1426 RVA: 0x00007A60 File Offset: 0x00005C60
	internal static void smethod_99(int int_19)
	{
		Class72.int_4 = int_19;
	}

	// Token: 0x06000593 RID: 1427 RVA: 0x00007A68 File Offset: 0x00005C68
	internal static string smethod_100()
	{
		return Class72.string_21;
	}

	// Token: 0x06000594 RID: 1428 RVA: 0x00007A6F File Offset: 0x00005C6F
	internal static void smethod_101(string string_56)
	{
		Class72.string_21 = string_56;
	}

	// Token: 0x06000595 RID: 1429 RVA: 0x00007A77 File Offset: 0x00005C77
	internal static string smethod_102()
	{
		return Class72.string_22;
	}

	// Token: 0x06000596 RID: 1430 RVA: 0x00007A7E File Offset: 0x00005C7E
	internal static void smethod_103(string string_56)
	{
		Class72.string_22 = string_56;
	}

	// Token: 0x06000597 RID: 1431 RVA: 0x00007A86 File Offset: 0x00005C86
	internal static string[] smethod_104()
	{
		return Class72.string_23;
	}

	// Token: 0x06000598 RID: 1432 RVA: 0x00007A8D File Offset: 0x00005C8D
	internal static void smethod_105(string[] string_56)
	{
		Class72.string_23 = string_56;
	}

	// Token: 0x06000599 RID: 1433 RVA: 0x00007A95 File Offset: 0x00005C95
	internal static int smethod_106()
	{
		return Class72.int_5;
	}

	// Token: 0x0600059A RID: 1434 RVA: 0x00007A9C File Offset: 0x00005C9C
	internal static void smethod_107(int int_19)
	{
		Class72.int_5 = int_19;
	}

	// Token: 0x0600059B RID: 1435 RVA: 0x00007AA4 File Offset: 0x00005CA4
	internal static bool smethod_108()
	{
		return Class72.bool_18;
	}

	// Token: 0x0600059C RID: 1436 RVA: 0x00007AAB File Offset: 0x00005CAB
	internal static void smethod_109(bool bool_54)
	{
		Class72.bool_18 = bool_54;
	}

	// Token: 0x0600059D RID: 1437 RVA: 0x00007AB3 File Offset: 0x00005CB3
	internal static DateTime smethod_110()
	{
		return Class72.dateTime_5;
	}

	// Token: 0x0600059E RID: 1438 RVA: 0x00007ABA File Offset: 0x00005CBA
	internal static void smethod_111(DateTime dateTime_14)
	{
		Class72.dateTime_5 = dateTime_14;
	}

	// Token: 0x0600059F RID: 1439 RVA: 0x00007AC2 File Offset: 0x00005CC2
	internal static int smethod_112()
	{
		return Class72.int_6;
	}

	// Token: 0x060005A0 RID: 1440 RVA: 0x00007AC9 File Offset: 0x00005CC9
	internal static void smethod_113(int int_19)
	{
		Class72.int_6 = int_19;
	}

	// Token: 0x060005A1 RID: 1441 RVA: 0x00007AD1 File Offset: 0x00005CD1
	internal static bool smethod_114()
	{
		return Class72.bool_19;
	}

	// Token: 0x060005A2 RID: 1442 RVA: 0x00007AD8 File Offset: 0x00005CD8
	internal static void smethod_115(bool bool_54)
	{
		Class72.bool_19 = bool_54;
	}

	// Token: 0x060005A3 RID: 1443 RVA: 0x00007AE0 File Offset: 0x00005CE0
	internal static string smethod_116()
	{
		return Class72.string_24;
	}

	// Token: 0x060005A4 RID: 1444 RVA: 0x00007AE7 File Offset: 0x00005CE7
	internal static void smethod_117(string string_56)
	{
		Class72.string_24 = string_56;
	}

	// Token: 0x060005A5 RID: 1445 RVA: 0x00007AEF File Offset: 0x00005CEF
	internal static string smethod_118()
	{
		return Class72.string_25;
	}

	// Token: 0x060005A6 RID: 1446 RVA: 0x00007AF6 File Offset: 0x00005CF6
	internal static void smethod_119(string string_56)
	{
		Class72.string_25 = string_56;
	}

	// Token: 0x060005A7 RID: 1447 RVA: 0x00007AFE File Offset: 0x00005CFE
	internal static string smethod_120()
	{
		return Class72.string_26;
	}

	// Token: 0x060005A8 RID: 1448 RVA: 0x00007B05 File Offset: 0x00005D05
	internal static void smethod_121(string string_56)
	{
		Class72.string_26 = string_56;
	}

	// Token: 0x060005A9 RID: 1449 RVA: 0x00007B0D File Offset: 0x00005D0D
	internal static string smethod_122()
	{
		return Class72.string_27;
	}

	// Token: 0x060005AA RID: 1450 RVA: 0x00007B14 File Offset: 0x00005D14
	internal static void smethod_123(string string_56)
	{
		Class72.string_27 = string_56;
	}

	// Token: 0x060005AB RID: 1451 RVA: 0x00007B1C File Offset: 0x00005D1C
	internal static bool smethod_124()
	{
		return Class72.bool_20;
	}

	// Token: 0x060005AC RID: 1452 RVA: 0x00007B23 File Offset: 0x00005D23
	internal static void smethod_125(bool bool_54)
	{
		Class72.bool_20 = bool_54;
	}

	// Token: 0x060005AD RID: 1453 RVA: 0x00007B2B File Offset: 0x00005D2B
	internal static string smethod_126()
	{
		return Class72.string_28;
	}

	// Token: 0x060005AE RID: 1454 RVA: 0x00007B32 File Offset: 0x00005D32
	internal static void smethod_127(string string_56)
	{
		Class72.string_28 = string_56;
	}

	// Token: 0x060005AF RID: 1455 RVA: 0x00007B3A File Offset: 0x00005D3A
	internal static int smethod_128()
	{
		return Class72.int_7;
	}

	// Token: 0x060005B0 RID: 1456 RVA: 0x00007B41 File Offset: 0x00005D41
	internal static void smethod_129(int int_19)
	{
		Class72.int_7 = int_19;
	}

	// Token: 0x060005B1 RID: 1457 RVA: 0x00007B49 File Offset: 0x00005D49
	internal static DateTime smethod_130()
	{
		return Class72.dateTime_6;
	}

	// Token: 0x060005B2 RID: 1458 RVA: 0x00007B50 File Offset: 0x00005D50
	internal static void smethod_131(DateTime dateTime_14)
	{
		Class72.dateTime_6 = dateTime_14;
	}

	// Token: 0x060005B3 RID: 1459 RVA: 0x00007B58 File Offset: 0x00005D58
	internal static DateTime smethod_132()
	{
		return Class72.dateTime_7;
	}

	// Token: 0x060005B4 RID: 1460 RVA: 0x00007B5F File Offset: 0x00005D5F
	internal static void smethod_133(DateTime dateTime_14)
	{
		Class72.dateTime_7 = dateTime_14;
	}

	// Token: 0x060005B5 RID: 1461 RVA: 0x00007B67 File Offset: 0x00005D67
	internal static string smethod_134()
	{
		return Class72.string_34;
	}

	// Token: 0x060005B6 RID: 1462 RVA: 0x00007B6E File Offset: 0x00005D6E
	internal static void smethod_135(string string_56)
	{
		Class72.string_34 = string_56;
	}

	// Token: 0x060005B7 RID: 1463 RVA: 0x00007B76 File Offset: 0x00005D76
	internal static bool smethod_136()
	{
		return Class72.bool_27;
	}

	// Token: 0x060005B8 RID: 1464 RVA: 0x00007B7D File Offset: 0x00005D7D
	internal static void smethod_137(bool bool_54)
	{
		Class72.bool_27 = bool_54;
	}

	// Token: 0x060005B9 RID: 1465 RVA: 0x00007B85 File Offset: 0x00005D85
	internal static bool smethod_138()
	{
		return Class72.bool_28;
	}

	// Token: 0x060005BA RID: 1466 RVA: 0x00007B8C File Offset: 0x00005D8C
	internal static void smethod_139(bool bool_54)
	{
		Class72.bool_28 = bool_54;
	}

	// Token: 0x060005BB RID: 1467 RVA: 0x00007B94 File Offset: 0x00005D94
	internal static bool smethod_140()
	{
		return Class72.bool_29;
	}

	// Token: 0x060005BC RID: 1468 RVA: 0x00007B9B File Offset: 0x00005D9B
	internal static void smethod_141(bool bool_54)
	{
		Class72.bool_29 = bool_54;
	}

	// Token: 0x060005BD RID: 1469 RVA: 0x00007BA3 File Offset: 0x00005DA3
	internal static int smethod_142()
	{
		return Class72.int_10;
	}

	// Token: 0x060005BE RID: 1470 RVA: 0x00007BAA File Offset: 0x00005DAA
	internal static void smethod_143(int int_19)
	{
		Class72.int_10 = int_19;
	}

	// Token: 0x060005BF RID: 1471 RVA: 0x00007BB2 File Offset: 0x00005DB2
	internal static Thread smethod_144()
	{
		return Class72.thread_0;
	}

	// Token: 0x060005C0 RID: 1472 RVA: 0x00007BB9 File Offset: 0x00005DB9
	internal static void smethod_145(Thread thread_1)
	{
		Class72.thread_0 = thread_1;
	}

	// Token: 0x060005C1 RID: 1473 RVA: 0x00007BC1 File Offset: 0x00005DC1
	internal static int[] smethod_146()
	{
		return Class72.int_11;
	}

	// Token: 0x060005C2 RID: 1474 RVA: 0x00007BC8 File Offset: 0x00005DC8
	internal static void smethod_147(int[] int_19)
	{
		Class72.int_11 = int_19;
	}

	// Token: 0x060005C3 RID: 1475 RVA: 0x00007BD0 File Offset: 0x00005DD0
	internal static DateTime smethod_148()
	{
		return Class72.dateTime_8;
	}

	// Token: 0x060005C4 RID: 1476 RVA: 0x00007BD7 File Offset: 0x00005DD7
	internal static void smethod_149(DateTime dateTime_14)
	{
		Class72.dateTime_8 = dateTime_14;
	}

	// Token: 0x040002BF RID: 703
	//internal static readonly Class55 class55_0 = new Class55(Class68.string_0, Application.ProductVersion);

	// Token: 0x040002C0 RID: 704
	internal static readonly Encoding encoding_0 = Encoding.GetEncoding(1251);

	// Token: 0x040002C1 RID: 705
	internal static readonly CultureInfo cultureInfo_0 = CultureInfo.GetCultureInfo("ru-RU");

	// Token: 0x040002C2 RID: 706
	internal static readonly CultureInfo cultureInfo_1 = CultureInfo.GetCultureInfo("en-US");

	// Token: 0x040002C3 RID: 707
	//internal static Class19 class19_0;

	// Token: 0x040002C4 RID: 708
	internal static WebProxy webProxy_0;

	// Token: 0x040002C5 RID: 709
	internal static FormMain formMain_0;

	// Token: 0x040002C6 RID: 710
	internal static string string_0;

	// Token: 0x040002C7 RID: 711
	internal static string string_1;

	// Token: 0x040002C8 RID: 712
	internal static string string_2;

	// Token: 0x040002C9 RID: 713
	internal static string string_3;

	// Token: 0x040002CA RID: 714
	internal static DateTime dateTime_0;

	// Token: 0x040002CB RID: 715
	internal static string string_4;

	// Token: 0x040002CC RID: 716
	private static ClearExplorerCacheForm clearExplorerCacheForm_0;

	// Token: 0x040002CD RID: 717
	private static bool bool_0;

	// Token: 0x040002CE RID: 718
	private static DateTime dateTime_1;

	// Token: 0x040002CF RID: 719
	private static string string_5;

	// Token: 0x040002D0 RID: 720
	private static bool bool_1;

	// Token: 0x040002D1 RID: 721
	private static bool bool_2;

	// Token: 0x040002D2 RID: 722
	private static bool bool_3;

	// Token: 0x040002D3 RID: 723
	private static DateTime dateTime_2;

	// Token: 0x040002D4 RID: 724
	private static string string_6;

	// Token: 0x040002D5 RID: 725
	//private static Enum4 enum4_0;

	// Token: 0x040002D6 RID: 726
	private static string string_7;

	// Token: 0x040002D7 RID: 727
	private static string string_8;

	// Token: 0x040002D8 RID: 728
	private static byte[] byte_0;

	// Token: 0x040002D9 RID: 729
	private static int int_0;

	// Token: 0x040002DA RID: 730
	private static DateTime dateTime_3;

	// Token: 0x040002DB RID: 731
	private static bool bool_4;

	// Token: 0x040002DC RID: 732
	private static bool bool_5;

	// Token: 0x040002DD RID: 733
	private static bool bool_6;

	// Token: 0x040002DE RID: 734
	private static bool bool_7;

	// Token: 0x040002DF RID: 735
	private static string string_9;

	// Token: 0x040002E0 RID: 736
	private static string string_10;

	// Token: 0x040002E1 RID: 737
	private static int int_1;

	// Token: 0x040002E2 RID: 738
	private static CityGateType cityGateType_0;

	// Token: 0x040002E3 RID: 739
	private static MapPath mapPath_0;

	// Token: 0x040002E4 RID: 740
	private static string string_11;

	// Token: 0x040002E5 RID: 741
	private static bool bool_8;

	// Token: 0x040002E6 RID: 742
	private static int int_2;

	// Token: 0x040002E7 RID: 743
	private static bool bool_9;

	// Token: 0x040002E8 RID: 744
	private static bool bool_10;

	// Token: 0x040002E9 RID: 745
	private static DateTime dateTime_4;

	// Token: 0x040002EA RID: 746
	private static bool bool_11;

	// Token: 0x040002EB RID: 747
	internal static readonly Dictionary<string, double> dictionary_0 = new Dictionary<string, double>();

	// Token: 0x040002EC RID: 748
	private static bool bool_12;

	// Token: 0x040002ED RID: 749
	private static bool bool_13;

	// Token: 0x040002EE RID: 750
	private static bool bool_14;

	// Token: 0x040002EF RID: 751
	private static string string_12;

	// Token: 0x040002F0 RID: 752
	private static string string_13;

	// Token: 0x040002F1 RID: 753
	private static string string_14;

	// Token: 0x040002F2 RID: 754
	private static string string_15;

	// Token: 0x040002F3 RID: 755
	private static string string_16;

	// Token: 0x040002F4 RID: 756
	private static string string_17;

	// Token: 0x040002F5 RID: 757
	private static string string_18;

	// Token: 0x040002F6 RID: 758
	private static double double_0;

	// Token: 0x040002F7 RID: 759
	private static bool bool_15;

	// Token: 0x040002F8 RID: 760
	private static bool bool_16;

	// Token: 0x040002F9 RID: 761
	private static bool bool_17;

	// Token: 0x040002FA RID: 762
	private static string string_19;

	// Token: 0x040002FB RID: 763
	private static string string_20;

	// Token: 0x040002FC RID: 764
	private static Dictionary<string, string> dictionary_1;

	// Token: 0x040002FD RID: 765
	private static int int_3;

	// Token: 0x040002FE RID: 766
	private static int int_4;

	// Token: 0x040002FF RID: 767
	private static string string_21;

	// Token: 0x04000300 RID: 768
	private static string string_22;

	// Token: 0x04000301 RID: 769
	private static string[] string_23;

	// Token: 0x04000302 RID: 770
	private static int int_5;

	// Token: 0x04000303 RID: 771
	private static bool bool_18;

	// Token: 0x04000304 RID: 772
	private static DateTime dateTime_5;

	// Token: 0x04000305 RID: 773
	private static int int_6;

	// Token: 0x04000306 RID: 774
	private static bool bool_19;

	// Token: 0x04000307 RID: 775
	private static string string_24;

	// Token: 0x04000308 RID: 776
	private static string string_25;

	// Token: 0x04000309 RID: 777
	private static string string_26;

	// Token: 0x0400030A RID: 778
	private static string string_27;

	// Token: 0x0400030B RID: 779
	private static bool bool_20;

	// Token: 0x0400030C RID: 780
	private static string string_28;

	// Token: 0x0400030D RID: 781
	private static int int_7;

	// Token: 0x0400030E RID: 782
	private static DateTime dateTime_6;

	// Token: 0x0400030F RID: 783
	private static DateTime dateTime_7;

	// Token: 0x04000310 RID: 784
	internal static FormCompas formCompas_0;

	// Token: 0x04000311 RID: 785
	internal static FormAddClan formAddClan_0;

	// Token: 0x04000312 RID: 786
	internal static bool bool_21;

	// Token: 0x04000313 RID: 787
	internal static string string_29;

	// Token: 0x04000314 RID: 788
	internal static string string_30;

	// Token: 0x04000315 RID: 789
	internal static int int_8;

	// Token: 0x04000316 RID: 790
	internal static bool bool_22;

	// Token: 0x04000317 RID: 791
	internal static bool bool_23;

	// Token: 0x04000318 RID: 792
	internal static bool bool_24;

	// Token: 0x04000319 RID: 793
	internal static bool bool_25;

	// Token: 0x0400031A RID: 794
	internal static string string_31;

	// Token: 0x0400031B RID: 795
	internal static string string_32;

	// Token: 0x0400031C RID: 796
	internal static bool bool_26;

	// Token: 0x0400031D RID: 797
	internal static string string_33;

	// Token: 0x0400031E RID: 798
	private static string string_34;

	// Token: 0x0400031F RID: 799
	private static bool bool_27;

	// Token: 0x04000320 RID: 800
	private static bool bool_28;

	// Token: 0x04000321 RID: 801
	private static bool bool_29;

	// Token: 0x04000322 RID: 802
	internal static bool bool_30;

	// Token: 0x04000323 RID: 803
	internal static bool bool_31;

	// Token: 0x04000324 RID: 804
	internal static string string_35;

	// Token: 0x04000325 RID: 805
	internal static int int_9;

	// Token: 0x04000326 RID: 806
	private static int int_10;

	// Token: 0x04000327 RID: 807
	private static Thread thread_0;

	// Token: 0x04000328 RID: 808
	private static int[] int_11;

	// Token: 0x04000329 RID: 809
	private static DateTime dateTime_8;

	// Token: 0x0400032A RID: 810
	internal static DateTime dateTime_9;

	// Token: 0x0400032B RID: 811
	internal static bool bool_32;

	// Token: 0x0400032C RID: 812
	internal static bool bool_33;

	// Token: 0x0400032D RID: 813
	internal static DateTime dateTime_10;

	// Token: 0x0400032E RID: 814
	internal static DateTime dateTime_11;

	// Token: 0x0400032F RID: 815
	internal static bool bool_34;

	// Token: 0x04000330 RID: 816
	internal static bool bool_35 = false;

	// Token: 0x04000331 RID: 817
	internal static int int_12;

	// Token: 0x04000332 RID: 818
	internal static readonly List<ShopEntry> list_0 = new List<ShopEntry>();

	// Token: 0x04000333 RID: 819
	internal static string string_36;

	// Token: 0x04000334 RID: 820
	internal static string string_37;

	// Token: 0x04000335 RID: 821
	internal static string string_38;

	// Token: 0x04000336 RID: 822
	internal static string string_39;

	// Token: 0x04000337 RID: 823
	internal static string string_40;

	// Token: 0x04000338 RID: 824
	internal static DateTime dateTime_12 = DateTime.MinValue;

	// Token: 0x04000339 RID: 825
	internal static DateTime dateTime_13 = DateTime.MinValue;

	// Token: 0x0400033A RID: 826
	internal static int int_13 = 0;

	// Token: 0x0400033B RID: 827
	internal static string string_41;

	// Token: 0x0400033C RID: 828
	internal static string string_42;

	// Token: 0x0400033D RID: 829
	internal static bool bool_36 = false;

	// Token: 0x0400033E RID: 830
	internal static bool bool_37 = true;

	// Token: 0x0400033F RID: 831
	internal static int int_14;

	// Token: 0x04000340 RID: 832
	internal static int int_15;

	// Token: 0x04000341 RID: 833
	internal static bool bool_38;

	// Token: 0x04000342 RID: 834
	internal static bool bool_39;

	// Token: 0x04000343 RID: 835
	internal static bool bool_40;

	// Token: 0x04000344 RID: 836
	internal static string string_43;

	// Token: 0x04000345 RID: 837
	internal static string string_44;

	// Token: 0x04000346 RID: 838
	internal static bool bool_41 = false;

	// Token: 0x04000347 RID: 839
	internal static bool bool_42 = false;

	// Token: 0x04000348 RID: 840
	internal static bool bool_43 = false;

	// Token: 0x04000349 RID: 841
	//internal static Class4 class4_0;

	// Token: 0x0400034A RID: 842
	//internal static Class79 class79_0 = null;

	// Token: 0x0400034B RID: 843
	internal static bool bool_44 = false;

	// Token: 0x0400034C RID: 844
	//internal static Class78 class78_0;

	// Token: 0x0400034D RID: 845
	//internal static Class80 class80_0;

	// Token: 0x0400034E RID: 846
	internal static bool bool_45 = false;

	// Token: 0x0400034F RID: 847
	internal static string string_45 = string.Empty;

	// Token: 0x04000350 RID: 848
	internal static string string_46 = string.Empty;

	// Token: 0x04000351 RID: 849
	internal static Dictionary<string, string> dictionary_2 = new Dictionary<string, string>();

	// Token: 0x04000352 RID: 850
	//internal static string string_47 = Class68.string_5;

	// Token: 0x04000353 RID: 851
	internal static bool bool_46 = true;

	// Token: 0x04000354 RID: 852
	internal static bool bool_47 = true;

	// Token: 0x04000355 RID: 853
	internal static bool bool_48 = true;

	// Token: 0x04000356 RID: 854
	internal static bool bool_49;

	// Token: 0x04000357 RID: 855
	internal static bool[] bool_50 = new bool[]
	{
		true,
		true,
		true
	};

	// Token: 0x04000358 RID: 856
	internal static string string_48;

	// Token: 0x04000359 RID: 857
	internal static string string_49 = string.Empty;

	// Token: 0x0400035A RID: 858
	internal static string string_50 = null;

	// Token: 0x0400035B RID: 859
	internal static bool bool_51;

	// Token: 0x0400035C RID: 860
	internal static bool bool_52;

	// Token: 0x0400035D RID: 861
	internal static string string_51 = string.Empty;

	// Token: 0x0400035E RID: 862
	internal static string string_52 = string.Empty;

	// Token: 0x0400035F RID: 863
	internal static Dictionary<string, int> dictionary_3 = new Dictionary<string, int>();

	// Token: 0x04000360 RID: 864
	internal static int int_16 = 0;

	// Token: 0x04000361 RID: 865
	internal static int int_17 = 0;

	// Token: 0x04000362 RID: 866
	internal static Dictionary<string, string> dictionary_4 = new Dictionary<string, string>();

	// Token: 0x04000363 RID: 867
	internal static string string_53 = string.Empty;

	// Token: 0x04000364 RID: 868
	internal static string string_54 = string.Empty;

	// Token: 0x04000365 RID: 869
	internal static string string_55 = string.Empty;

	// Token: 0x04000366 RID: 870
	internal static bool bool_53 = false;

	// Token: 0x04000367 RID: 871
	internal static int int_18 = 0;
}
