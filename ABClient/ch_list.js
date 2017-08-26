document.onmousedown = t_nick;

function t_nick ()
{
  top.is_ctrl = window.event.ctrlKey;
  top.is_alt = window.event.altKey;
}

function ch_clear_ignor (nick)
{
  while (nick.indexOf ('=') >= 0) nick = nick.replace ('=', '%3D');
  while (nick.indexOf ('+') >= 0) nick = nick.replace ('+', '%2B');
  while (nick.indexOf ('#') >= 0) nick = nick.replace ('#', '%23');
  while (nick.indexOf (' ') >= 0) nick = nick.replace (' ', '%20');
  top.frames['ch_refr'].location='./ch.php?a=ign&s=0&u='+nick;
}
  
function reverse_alpha_sort (el1,el2) {
    if (el1 > el2) {
        return -1;
    } else if (el1 < el2) {
        return 1;
    } else {
        return 0;
    }
}

function qsort_str(arr,first,last) {
 if (first<last) {
  point=arr[first].split(":")[0];
  i=first;
  j=last;
  while (i<j) {
   while ((arr[i].split(":")[0]<=point) && (i<last)) i++;
   while ((arr[j].split(":")[0]>=point) && (j>first)) j--;
   if (i<j) {
    temp=arr[i];
    arr[i]=arr[j];
    arr[j]=temp;
   }
  }
  temp=arr[first];
  arr[first]=arr[j];
  arr[j]=temp;
  qsort_str(arr,first,j-1);
  qsort_str(arr,j+1,last);
 }
}

function qsort_int(arr,first,last,h) {
 if (first<last) {
  point=parseInt(arr[first].split(":")[2]);
  i=first;
  j=last;
  while (i<j) {
   while ((h*parseInt(arr[i].split(":")[2])<=h*point) && (i<last)) i++;
   while ((h*parseInt(arr[j].split(":")[2])>=h*point) && (j>first)) j--;
   if (i<j) {
    temp=arr[i];
    arr[i]=arr[j];
    arr[j]=temp;
   }
  }
  temp=arr[first];
  arr[first]=arr[j];
  arr[j]=temp;
  qsort_int(arr,first,j-1,h);
  qsort_int(arr,j+1,last,h);
 }
}

function chatlist_build (sort_type)
{
  if (sort_type=='a_z') ChatListU.sort ();
  else if (sort_type=='z_a') ChatListU.sort (reverse_alpha_sort);
  else {

   if (sort_type=='0_33') {
    qsort_int(ChatListU,0,ChatListU.length-1,1);
   }
   else if (sort_type=='33_0') {
    qsort_int(ChatListU,0,ChatListU.length-1,-1);
   }

   f=0;
   fl=parseInt(ChatListU[f].split(":")[2]);
   for (i=1;i<ChatListU.length;i++) {
    n=i;
    nl=parseInt(ChatListU[i].split(":")[2]);
    if (fl!=nl) {
     qsort_str(ChatListU,f,n-1);
     f=n;
     fl=parseInt(ChatListU[f].split(":")[2]);
    }
    if (n==ChatListU.length-1) {
     qsort_str(ChatListU,f,n);
    }
   }
  }

  var ss;
  var sleeps;
  var nn_sec;
  var str_array;
  var sign_array;
  var altadd;

  var dd0 = "";
  var dd1 = "";
  var dd2 = "";
  var ddn = "";

  for(var cou = 0; cou < ChatListU.length; cou++)
  {
    str_array = ChatListU[cou].split(":");

    var ss='';
    var sleeps='';
    var altadd='';
    var ign = '';
    var inj = '';
    var psg = '';
    var align = '';

    nn_sec = str_array[1];
    var login = str_array[1];
    while (nn_sec.indexOf('+')>=0) nn_sec = nn_sec.replace('+','%2B');
    if (login.indexOf ('<i>') > -1)
    {
      login = login.replace ('<i>', '');
      login = login.replace ('</i>', '');
      nn_sec = nn_sec.replace ('<i>', '');
      nn_sec = nn_sec.replace ('</i>', '');
    }

    if (str_array[3].length>1)
    {
      sign_array = str_array[3].split(";");
      if(sign_array[2].length>1)
        altadd=" ("+sign_array[2]+")";
      ss = "<img src=http://image.neverlands.ru/signs/"+sign_array[0]+" width=15 height=12 align=absmiddle title=\""+sign_array[1]+altadd+"\">&nbsp;";
    }

    if(str_array[4].length>1)
      sleeps="<img src=http://image.neverlands.ru/signs/molch.gif width=15 height=12 border=0 title=\""+str_array[4]+"\" align=absmiddle>";
    if (str_array[5] == '1')
      ign = "<a href=\"javascript:ch_clear_ignor('"+login+"');\"><img src=http://image.neverlands.ru/signs/ignor/3.gif width=15 height=12 border=0 title=\"Снять игнорирование\"></a>";
    if (str_array[6] != '0')
    {
       inj = "<img src=http://image.neverlands.ru/chat/tr4.gif border=0 width=15 height=12 alt=\""+str_array[6]+"\" align=absmiddle>";
    }

     if (str_array[7] != '0')
     {
       var dilers = new Array ('', 'Дилер', '', '', '', '', '', '', '', '', '', 'Помощник дилера');
       psg = "<img src=http://image.neverlands.ru/signs/d_sm_"+str_array[7]+".gif width=15 height=12 align=absmiddle border=0 alt=\""+dilers[str_array[7]]+"\">&nbsp;";
     }
     if (str_array[8] != '0')
     {
       sign_array = str_array[8].split(";");
       align = "<img src=http://image.neverlands.ru/signs/"+sign_array[0]+" width=15 height=12 align=absmiddle border=0 alt=\""+sign_array[1]+"\">&nbsp";
     }
     else if (str_array[7] == '0')
         align = "<img src=http://image.neverlands.ru/1x1.gif width=15 height=12 align=absmiddle border=0 alt=\"\">&nbsp";

    var classid = window.external.GetClassIdOfContact(login);
    if (classid == 1) {
        str_array[1] = "<font color=\"#8A0808\">" + str_array[1] + "</font>";
    } else if (classid == 2) {
          str_array[1] = "<font color=\"#0B610B\">" + str_array[1] + "</font>";
    }


    var wmlabQ = "";
    var wmlabFA = ""; 
    var wmlabFAB = "";
    var wmlabFAU = "";
    var wmlabFACU = "";
    var wmlabFAF = "";
    var wmlabFACF = "";
    var wmlabFP = "";
    var wmlabFC = "";
    var wmlabFAP = "";
    var wmlabFAS = "";
    var wmlabFAN = "";
    var wmlabFAFG = "";
    var wmlabFAZ = "";
    var wmlabFTOT = "";

    if (classid == 2) {
        wmlabQ = " <a class=\"activeico\" href=\"javascript:window.external.Quick('" + login + "')\"><img src=http://image.neverlands.ru/signs/c227.gif width=15 height=12 border=0 alt='Быстрые действия' align=absmiddle></a>";
        wmlabFAN = " <a class=\"activeico\" href=\"javascript:window.external.FastAttackNevid('" + login + "')\"><img src=http://image.neverlands.ru/weapon/Bi_w27_53.gif width=15 height=15 border=0 alt='Зелье Невидимости' align=absmiddle></a>";
        wmlabFAFG = " <a class=\"activeico\" href=\"javascript:window.external.FastAttackFog('" + login + "')\"><img src=http://image.neverlands.ru/weapon/i_svi_213.gif width=30 height=15 border=0 alt='Свиток искажающего тумана' align=absmiddle></a>";
        wmlabFAZ = " <a class=\"activeico\" href=\"javascript:window.external.FastAttackZas('" + login + "')\"><img src=http://image.neverlands.ru/weapon/i_w28_27.gif width=30 height=15 border=0 alt='Свиток защиты' align=absmiddle></a>";
        dd2 = dd2 +
            window.external.CheckQuick(login, wmlabQ) +
            window.external.CheckFastAttackNevid(login, wmlabFAN) +
            window.external.CheckFastAttackFog(login, wmlabFAFG) +
            window.external.CheckFastAttackZas(login, wmlabFAZ) +
            "<img src=http://image.neverlands.ru/1x1.gif width=5 height=0>" +
            "<a href=\"javascript:top.say_private('" + login + "')\"><img src=http://image.neverlands.ru/chat/private.gif width=11 height=12 border=0 align=absmiddle></a>&nbsp;" +
            psg +
            align +
            ss +
            "<a class=\"activenick\" href=\"javascript:top.say_to('" + login + "')\"><font class=nickname><b>" +
            str_array[1] +
            "</b></a>[" +
            str_array[2] +
            "]</font><a href=\"/pinfo.cgi?" + nn_sec +
            "\" target=_blank><img src=http://image.neverlands.ru/chat/info.gif width=11 height=12 border=0 align=absmiddle></a>" + sleeps + "&nbsp;" + ign + "&nbsp;" + inj +
            "<br>";
    }

    if (classid == 1) {
        wmlabQ = " <a class=\"activeico\" href=\"javascript:window.external.Quick('" + login + "')\"><img src=http://image.neverlands.ru/signs/c227.gif width=15 height=12 border=0 alt='Быстрые действия' align=absmiddle></a>";
        wmlabFA = " <a class=\"activeico\" href=\"javascript:window.external.FastAttack('" + login + "')\"><img src=http://image.neverlands.ru/weapon/i_svi_001.gif width=30 height=15 border=0 alt='Обычное нападение' align=absmiddle></a>";
        wmlabFAB = " <a class=\"activeico\" href=\"javascript:window.external.FastAttackBlood('" + login + "')\"><img src=http://image.neverlands.ru/weapon/i_svi_002.gif width=30 height=15 border=0 alt='Кровавое нападение' align=absmiddle></a>";
        wmlabFAU = " <a class=\"activeico\" href=\"javascript:window.external.FastAttackUltimate('" + login + "')\"><img src=http://image.neverlands.ru/weapon/i_w28_26.gif width=30 height=15 border=0 alt='Боевое нападение' align=absmiddle></a>";
        wmlabFACU = " <a class=\"activeico\" href=\"javascript:window.external.FastAttackClosedUltimate('" + login + "')\"><img src=http://image.neverlands.ru/weapon/i_w28_26.gif width=30 height=15 border=0 alt='Закрытое боевое нападение' align=absmiddle></a>";
        wmlabFAF = " <a class=\"activeico\" href=\"javascript:window.external.FastAttackFist('" + login + "')\"><img src=http://image.neverlands.ru/weapon/i_w28_24.gif width=30 height=15 border=0 alt='Кулачное нападение' align=absmiddle></a>";
        wmlabFACF = " <a class=\"activeico\" href=\"javascript:window.external.FastAttackClosedFist('" + login + "')\"><img src=http://image.neverlands.ru/weapon/i_w28_25.gif width=30 height=15 border=0 alt='Закрытое кулачное нападение' align=absmiddle></a>";
        wmlabFP = " <a class=\"activeico\" href=\"javascript:window.external.FastAttackPortal('" + login + "')\"><img src=http://image.neverlands.ru/weapon/i_w28_86.gif width=30 height=15 border=0 alt='Телепорт' align=absmiddle></a>";
        wmlabFC = " <a class=\"activeico\" href=\"javascript:window.external.FastAttackClosed('" + login + "')\"><img src=http://image.neverlands.ru/weapon/i_svi_205.gif width=30 height=15 border=0 alt='Закрытое нападение' align=absmiddle></a>";
        wmlabFAP = " <a class=\"activeico\" href=\"javascript:window.external.FastAttackPoison('" + login + "')\"><img src=http://image.neverlands.ru/weapon/Bi_w27_41.gif width=15 height=15 border=0 alt='Яд' align=absmiddle></a>";
        wmlabFAS = " <a class=\"activeico\" href=\"javascript:window.external.FastAttackStrong('" + login + "')\"><img src=http://image.neverlands.ru/weapon/Bi_w27_52.gif width=15 height=15 border=0 alt='Зелье Сильной Спины' align=absmiddle></a>";
        wmlabFAN = " <a class=\"activeico\" href=\"javascript:window.external.FastAttackNevid('" + login + "')\"><img src=http://image.neverlands.ru/weapon/Bi_w27_53.gif width=15 height=15 border=0 alt='Зелье Невидимости' align=absmiddle></a>";
        wmlabFAFG = " <a class=\"activeico\" href=\"javascript:window.external.FastAttackFog('" + login + "')\"><img src=http://image.neverlands.ru/weapon/i_svi_213.gif width=30 height=15 border=0 alt='Свиток искажающего тумана' align=absmiddle></a>";
        wmlabFAZ = " <a class=\"activeico\" href=\"javascript:window.external.FastAttackZas('" + login + "')\"><img src=http://image.neverlands.ru/weapon/i_w28_27.gif width=30 height=15 border=0 alt='Свиток защиты' align=absmiddle></a>";
        wmlabFTOT = " <a class=\"activeico\" href=\"javascript:window.external.FastAttackTotem('" + login + "')\"><img src=http://image.neverlands.ru/signs/totems/9.gif width=15 height=15 border=0 alt='Тотемное нападение' align=absmiddle></a>";
        dd1 = dd1 +
            window.external.CheckQuick(login, wmlabQ) +
            window.external.CheckFastAttack(login, wmlabFA) +
            window.external.CheckFastAttackBlood(login, wmlabFAB) +
            window.external.CheckFastAttackUltimate(login, wmlabFAU) +
            window.external.CheckFastAttackClosedUltimate(login, wmlabFACU) +
            window.external.CheckFastAttackFist(login, wmlabFAF) +
            window.external.CheckFastAttackClosedFist(login, wmlabFACF) +
	    window.external.CheckFastAttackPortal(login, wmlabFP) +
            window.external.CheckFastAttackClosed(login, wmlabFC) +
            window.external.CheckFastAttackPoison(login, wmlabFAP) +
            window.external.CheckFastAttackStrong(login, wmlabFAS) +
            window.external.CheckFastAttackNevid(login, wmlabFAN) +
            window.external.CheckFastAttackFog(login, wmlabFAFG) +            
            window.external.CheckFastAttackTotem(login, wmlabFTOT) +
            "<img src=http://image.neverlands.ru/1x1.gif width=5 height=0>" +
            "<a href=\"javascript:top.say_private('" + login + "')\"><img src=http://image.neverlands.ru/chat/private.gif width=11 height=12 border=0 align=absmiddle></a>&nbsp;" +
            psg +
            align +
            ss +
            "<a class=\"activenick\" href=\"javascript:top.say_to('" + login + "')\"><font class=nickname><b>" +
            str_array[1] +
            "</b></a>[" +
            str_array[2] +
            "]</font><a href=\"/pinfo.cgi?" + nn_sec +
            "\" target=_blank><img src=http://image.neverlands.ru/chat/info.gif width=11 height=12 border=0 align=absmiddle></a>" + sleeps + "&nbsp;" + ign + "&nbsp;" + inj +
            "<br>";
    }

    if (classid == 0) {
        wmlabQ = " <a class=\"activeico\" href=\"javascript:window.external.Quick('" + login + "')\"><img src=http://image.neverlands.ru/signs/c227.gif width=15 height=12 border=0 alt='Быстрые действия' align=absmiddle></a>";
        wmlabFA = " <a class=\"activeico\" href=\"javascript:window.external.FastAttack('" + login + "')\"><img src=http://image.neverlands.ru/weapon/i_svi_001.gif width=30 height=15 border=0 alt='Обычное нападение' align=absmiddle></a>";
        wmlabFAB = " <a class=\"activeico\" href=\"javascript:window.external.FastAttackBlood('" + login + "')\"><img src=http://image.neverlands.ru/weapon/i_svi_002.gif width=30 height=15 border=0 alt='Кровавое нападение' align=absmiddle></a>";
        wmlabFAU = " <a class=\"activeico\" href=\"javascript:window.external.FastAttackUltimate('" + login + "')\"><img src=http://image.neverlands.ru/weapon/i_w28_26.gif width=30 height=15 border=0 alt='Боевое нападение' align=absmiddle></a>";
        wmlabFACU = " <a class=\"activeico\" href=\"javascript:window.external.FastAttackClosedUltimate('" + login + "')\"><img src=http://image.neverlands.ru/weapon/i_w28_26.gif width=30 height=15 border=0 alt='Закрытое боевое нападение' align=absmiddle></a>";
        wmlabFAF = " <a class=\"activeico\" href=\"javascript:window.external.FastAttackFist('" + login + "')\"><img src=http://image.neverlands.ru/weapon/i_w28_24.gif width=30 height=15 border=0 alt='Кулачное нападение' align=absmiddle></a>";
        wmlabFACF = " <a class=\"activeico\" href=\"javascript:window.external.FastAttackClosedFist('" + login + "')\"><img src=http://image.neverlands.ru/weapon/i_w28_25.gif width=30 height=15 border=0 alt='Закрытое кулачное нападение' align=absmiddle></a>";
        wmlabFP = " <a class=\"activeico\" href=\"javascript:window.external.FastAttackPortal('" + login + "')\"><img src=http://image.neverlands.ru/weapon/i_w28_86.gif width=30 height=15 border=0 alt='Портал' align=absmiddle></a>";
        wmlabFC = " <a class=\"activeico\" href=\"javascript:window.external.FastAttackClosed('" + login + "')\"><img src=http://image.neverlands.ru/weapon/i_svi_205.gif width=30 height=15 border=0 alt='Закрытое нападение' align=absmiddle></a>";
        wmlabFAP = " <a class=\"activeico\" href=\"javascript:window.external.FastAttackPoison('" + login + "')\"><img src=http://image.neverlands.ru/weapon/Bi_w27_41.gif width=15 height=15 border=0 alt='Яд' align=absmiddle></a>";
        wmlabFAS = " <a class=\"activeico\" href=\"javascript:window.external.FastAttackStrong('" + login + "')\"><img src=http://image.neverlands.ru/weapon/Bi_w27_52.gif width=15 height=15 border=0 alt='Зелье Сильной Спины' align=absmiddle></a>";
        wmlabFAN = " <a class=\"activeico\" href=\"javascript:window.external.FastAttackNevid('" + login + "')\"><img src=http://image.neverlands.ru/weapon/Bi_w27_53.gif width=15 height=15 border=0 alt='Зелье Невидимости' align=absmiddle></a>";
        wmlabFAFG = " <a class=\"activeico\" href=\"javascript:window.external.FastAttackFog('" + login + "')\"><img src=http://image.neverlands.ru/weapon/i_svi_213.gif width=30 height=15 border=0 alt='Свиток искажающего тумана' align=absmiddle></a>";
        wmlabFAZ = " <a class=\"activeico\" href=\"javascript:window.external.FastAttackZas('" + login + "')\"><img src=http://image.neverlands.ru/weapon/i_w28_27.gif width=30 height=15 border=0 alt='Свиток защиты' align=absmiddle></a>";
        wmlabFTOT = " <a class=\"activeico\" href=\"javascript:window.external.FastAttackTotem('" + login + "')\"><img src=http://image.neverlands.ru/signs/totems/9.gif width=15 height=15 border=0 alt='Тотемное нападение' align=absmiddle></a>";
        dd0 = dd0 +
            window.external.CheckQuick(login, wmlabQ) +
            window.external.CheckFastAttack(login, wmlabFA) +
            window.external.CheckFastAttackBlood(login, wmlabFAB) +
            window.external.CheckFastAttackUltimate(login, wmlabFAU) +
            window.external.CheckFastAttackClosedUltimate(login, wmlabFACU) +
            window.external.CheckFastAttackFist(login, wmlabFAF) +
            window.external.CheckFastAttackClosedFist(login, wmlabFACF) +
            window.external.CheckFastAttackPortal(login, wmlabFP) +
            window.external.CheckFastAttackClosed(login, wmlabFC) +
            window.external.CheckFastAttackPoison(login, wmlabFAP) +
            window.external.CheckFastAttackStrong(login, wmlabFAS) +
            window.external.CheckFastAttackNevid(login, wmlabFAN) +
            window.external.CheckFastAttackFog(login, wmlabFAFG) +
            window.external.CheckFastAttackZas(login, wmlabFAZ) +
            window.external.CheckFastAttackTotem(login, wmlabFTOT) +
            "<img src=http://image.neverlands.ru/1x1.gif width=5 height=0>" +
            "<a href=\"javascript:top.say_private('" + login + "')\"><img src=http://image.neverlands.ru/chat/private.gif width=11 height=12 border=0 align=absmiddle></a>&nbsp;" +
            psg +
            align +
            ss +
            "<a class=\"activenick\" href=\"javascript:top.say_to('" + login + "')\"><font class=nickname><b>" +
            str_array[1] +
            "</b></a>[" +
            str_array[2] +
            "]</font><a href=\"/pinfo.cgi?" + nn_sec +
            "\" target=_blank><img src=http://image.neverlands.ru/chat/info.gif width=11 height=12 border=0 align=absmiddle></a>" + sleeps + "&nbsp;" + ign + "&nbsp;" + inj +
            "<br>";
    }

    if (classid < 0) {
        wmlabQ = " <a class=\"activeico\" href=\"javascript:window.external.Quick('" + login + "')\"><img src=http://image.neverlands.ru/signs/c227.gif width=15 height=12 border=0 alt='Быстрые действия' align=absmiddle></a>";
        wmlabFA = " <a class=\"activeico\" href=\"javascript:window.external.FastAttack('" + login + "')\"><img src=http://image.neverlands.ru/weapon/i_svi_001.gif width=14 height=11 border=0 alt='Обычное нападение' align=absmiddle></a>"; 
        wmlabFAB = " <a class=\"activeico\" href=\"javascript:window.external.FastAttackBlood('" + login + "')\"><img src=http://image.neverlands.ru/weapon/i_svi_002.gif width=14 height=11 border=0 alt='Кровавое нападение' align=absmiddle></a>";
        wmlabFAU = " <a class=\"activeico\" href=\"javascript:window.external.FastAttackUltimate('" + login + "')\"><img src=http://image.neverlands.ru/weapon/i_w28_26.gif width=14 height=11 border=0 alt='Боевое нападение' align=absmiddle></a>";
        wmlabFACU = " <a class=\"activeico\" href=\"javascript:window.external.FastAttackClosedUltimate('" + login + "')\"><img src=http://image.neverlands.ru/weapon/i_w28_26.gif width=14 height=11 border=0 alt='Закрытое боевое нападение' align=absmiddle></a>";
        wmlabFP = " <a class=\"activeico\" href=\"javascript:window.external.FastAttackPortal('" + login + "')\"><img src=http://image.neverlands.ru/weapon/i_w28_86.gif width=30 height=15 border=0 alt='Портал' align=absmiddle></a>";
        wmlabFC = " <a class=\"activeico\" href=\"javascript:window.external.FastAttackClosed('" + login + "')\"><img src=http://image.neverlands.ru/weapon/i_svi_205.gif width=14 height=11 border=0 alt='Закрытое нападение' align=absmiddle></a>";
        wmlabFAF = " <a class=\"activeico\" href=\"javascript:window.external.FastAttackFist('" + login + "')\"><img src=http://image.neverlands.ru/weapon/i_w28_24.gif width=14 height=11 border=0 alt='Кулачное нападение' align=absmiddle></a>";
        wmlabFACF = " <a class=\"activeico\" href=\"javascript:window.external.FastAttackClosedFist('" + login + "')\"><img src=http://image.neverlands.ru/weapon/i_w28_25.gif width=14 height=11 border=0 alt='Закрытое кулачное нападение' align=absmiddle></a>";
        wmlabFAP = " <a class=\"activeico\" href=\"javascript:window.external.FastAttackPoison('" + login + "')\"><img src=http://image.neverlands.ru/weapon/Bi_w27_41.gif width=15 height=15 border=0 alt='Яд' align=absmiddle></a>";
        wmlabFAS = " <a class=\"activeico\" href=\"javascript:window.external.FastAttackStrong('" + login + "')\"><img src=http://image.neverlands.ru/weapon/Bi_w27_52.gif width=15 height=15 border=0 alt='Зелье Сильной Спины' align=absmiddle></a>";
        wmlabFAN = " <a class=\"activeico\" href=\"javascript:window.external.FastAttackNevid('" + login + "')\"><img src=http://image.neverlands.ru/weapon/Bi_w27_53.gif width=15 height=15 border=0 alt='Зелье Невидимости' align=absmiddle></a>";
        wmlabFAFG = " <a class=\"activeico\" href=\"javascript:window.external.FastAttackFog('" + login + "')\"><img src=http://image.neverlands.ru/weapon/i_svi_213.gif width=14 height=11 border=0 alt='Свиток искажающего тумана' align=absmiddle></a>";
        wmlabFAZ = " <a class=\"activeico\" href=\"javascript:window.external.FastAttackZas('" + login + "')\"><img src=http://image.neverlands.ru/weapon/i_w28_27.gif width=14 height=11 border=0 alt='Свиток защиты' align=absmiddle></a>";
        wmlabFTOT = " <a class=\"activeico\" href=\"javascript:window.external.FastAttackTotem('" + login + "')\"><img src=http://image.neverlands.ru/signs/totems/9.gif width=11 height=11 border=0 alt='Тотемное нападение' align=absmiddle></a>";
        ddn = ddn + "<img src=http://image.neverlands.ru/1x1.gif width=5 height=0>" +
            "<a href=\"javascript:top.say_private('"+login+"')\"><img src=http://image.neverlands.ru/chat/private.gif width=11 height=12 border=0 align=absmiddle></a>&nbsp;" + 
            psg + 
            align + 
            ss + 
            "<a class=\"activenick\" href=\"javascript:top.say_to('" + login + "')\"><font class=nickname><b>" + 
            str_array[1] + 
            "</b></a>[" + 
            str_array[2] + 
            "]</font><a href=\"/pinfo.cgi?" + nn_sec + 
            "\" target=_blank><img src=http://image.neverlands.ru/chat/info.gif width=11 height=12 border=0 align=absmiddle></a>" + sleeps + "&nbsp;" + ign + "&nbsp;" + inj +
            window.external.CheckQuick(login, wmlabQ) +
            window.external.CheckFastAttack(login, wmlabFA) + 
            window.external.CheckFastAttackBlood(login, wmlabFAB) +
            window.external.CheckFastAttackUltimate(login, wmlabFAU) +
            window.external.CheckFastAttackClosedUltimate(login, wmlabFACU) +  
            window.external.CheckFastAttackFist(login, wmlabFAF) +
            window.external.CheckFastAttackClosedFist(login, wmlabFACF) +
            window.external.CheckFastAttackPortal(login, wmlabFP) +
            window.external.CheckFastAttackClosed(login, wmlabFC) +
            window.external.CheckFastAttackPoison(login, wmlabFAP) +
            window.external.CheckFastAttackStrong(login, wmlabFAS) +
            window.external.CheckFastAttackNevid(login, wmlabFAN) +
            window.external.CheckFastAttackFog(login, wmlabFAFG) +
            window.external.CheckFastAttackZas(login, wmlabFAZ) +
            window.external.CheckFastAttackTotem(login, wmlabFTOT) +
            "<br>";
     }
  }
  
  document.write(dd1 + dd2 + dd0 + ddn);
}

function clipboardcopy(text2copy)
{
  if (window.clipboardData) {
    window.clipboardData.setData("Text",text2copy);
  }
}
