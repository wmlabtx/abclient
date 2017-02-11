var d = document;
var world = false;
var transport_img = false;
var timer_img = false;
var timer_sec = false;
var width = 3;
var height = 1;
var move_interval = 50;
var current_x = 0;
var current_y = 0;
var time_left = 0;
var time_left_sec = 0;
var pause = 0;
var t = 0;
var tsec = 0;
var cur_margin_top = 0;
var cur_margin_left = 0;
var dest_x = 0;
var dest_y = 0;
var loaded_left = 0;
var loaded_right = 0;
var loaded_top = 0;
var loaded_bottom = 0;
var moving_status = 0;
var finStatus = 0;
var gox = 0;
var goy = 0;
var gop = 0;
var avail = {};
var bavail = {};
var classn = false;
var MESSD = false;
var MDARK = false;
var rinit = 0;

var pngAlpha = 1;
var ua = navigator.userAgent.toLowerCase();

this.isIE = ((ua.indexOf('msie') != -1) && !(ua.indexOf('opera') != -1) && (ua.indexOf('webtv') == -1));
this.versionMinor = parseFloat(navigator.appVersion);
this.versionMajor = parseInt(navigator.appVersion);

if(this.isIE && this.versionMinor >= 4) this.versionMinor = parseFloat(ua.substring(ua.indexOf('msie ')+5));
if(this.isIE && parseInt(this.versionMinor)<7) pngAlpha = 0;

function view_build_top()
{
	if(build[11])
	{
		parent.frames["ch_list"].location = "/ch.php?lo=1";
	}

	ins_HP();
	d.write('<table cellpadding=4 cellspacing=0 border=0 width=100%><tr><td bgcolor=#FCFAF3><table cellpadding=0 cellspacing=0 border=0>');
	d.write('<tr><td rowspan=3><font class=nick>'+sh_align(build[2],0)+sh_sign(build[3],build[4],build[5])+'<B>'+build[0]+'</B>['+build[1]+']&nbsp;</font></td><td><img src=http://image.neverlands.ru/1x1.gif width=1 height=2><br><img src=http://image.neverlands.ru/gameplay/hp.gif width=0 height=6 border=0 id=fHP vspace=0 align=absmiddle><img src=http://image.neverlands.ru/gameplay/nohp.gif width=0 height=6 border=0 id=eHP vspace=0 align=absmiddle></td><td rowspan=3 class=hpbar><div id=hbar></div></td></tr>');
	d.write('<tr><td bgcolor=#ffffff><img src=http://image.neverlands.ru/1x1.gif width=1 height=1></td></tr>');
	d.write('<tr><td><img src=http://image.neverlands.ru/gameplay/ma.gif width=0 height=6 border=0 id=fMP vspace=0 align=absmiddle><img src=http://image.neverlands.ru/gameplay/noma.gif width=0 height=6 border=0 id=eMP vspace=0 align=absmiddle></td></tr>');
	d.write('</table></td><td bgcolor=#FCFAF3><div align=center id=ButtonPlace>'+ButtonGen()+'</div></td><td bgcolor=#FCFAF3><div align=right><a href="javascript: top.exit_redir()"><img src=http://image.neverlands.ru/exit.gif align=absmiddle width=15 height=15 border=0></a></div></td></tr></table>');
	cha_HP();
	    
	d.write('<table cellpadding=0 cellspacing=0 border=0 width=100%><tr><td bgcolor=#FFFFFF><img src=http://image.neverlands.ru/1x1.gif width=1 height=1></td></tr><tr><td bgcolor=#B9A05C><img src=http://image.neverlands.ru/1x1.gif width=1 height=1></td></tr><tr><td bgcolor=#F3ECD7><img src=http://image.neverlands.ru/1x1.gif width=1 height=2></td></tr><tr><td bgcolor=#FFFFFF><img src=http://image.neverlands.ru/1x1.gif width=1 height=10></td></tr></table>');
}

function view_build_bottom()
{
    d.write('<table cellpadding=0 cellspacing=0 border=0 width=100%><tr><td bgcolor=#FFFFFF><img src=http://image.neverlands.ru/1x1.gif width=1 height=4></td></tr><tr><td align=center>'+view_t()+'</td></tr><tr><td bgcolor=#FFFFFF><img src=http://image.neverlands.ru/1x1.gif width=1 height=10></td></tr></table>');
}

function view_map()
{
    view_build_top();

    var documentHeight = document.body.clientHeight;
    var documentWidth = document.body.clientWidth;

    width  = Math.max(1, Math.floor( ((documentWidth  / 100) - 1) / 2));
    height = Math.max(1, Math.floor( ((documentHeight / 100) - 1) / 2));

    var widthPx  = ((width  * 2) + 1) * 100;
    var heightPx = ((height * 2) + 1) * 100;
    d.write('<table cellpadding=0 cellspacing=0 border=0 width=100%><tr><td bgcolor=#FFFFFF align=center><div style="position: absolute; border: 1px solid black; overflow: hidden; width: '+widthPx+'px; height: '+heightPx+'px; left: 50%; margin-left: -'+(widthPx / 2)+'px;" id="world_cont"></div><div style="width: '+widthPx+'px; height: '+heightPx+'px; text-align: left;" id="world_cont2"></div></td></tr></table>');

    for(var i=0; i<map[1].length; i++)
    {
        avail[map[1][i][0]+'_'+map[1][i][1]] = map[1][i][2];
    }
    
    if(!map[0][4].length) 
    {
        current_x = map[0][0];
        current_y = map[0][1];
        showCursor();
        showMap(current_x, current_y);    
    }
    else if(!map[0][4][0])
    {
        finStatus = 1;
        showTransport('man', map[0][4][4], map[0][4][5], map[0][0], map[0][1], 8, 'gif'); 
        loadPath(map[0][4][4], map[0][4][5], map[0][0], map[0][1], (map[0][4][3] - map[0][4][2]), (map[0][4][3] - map[0][4][1]));
        TimerStart((map[0][4][3] - map[0][4][1]),0);   
    }
    else
    {
        // Работа или защита от подбора
        finStatus = 2;
        current_x = map[0][0];
        current_y = map[0][1];
        showCursor();
        showMap(current_x, current_y); 
        TimerStart(map[0][4][1],1);
    }
    
    if(map[0][5]) MessBoxDiv(map[0][5]);

    view_build_bottom();    
}

function ButtonGen()
{
    var str = '';
    bavail = {};
    for(var i=0; i<mapbt.length; i++)
    {
        bavail[mapbt[i][0]] = [mapbt[i][2],mapbt[i][3]];
        str += ' <input type=button class=fr_but id="'+mapbt[i][0]+'" value="'+mapbt[i][1]+'" onclick=\'ButClick("'+mapbt[i][0]+'")\'>';
    }
    return str;
}

function ButClick(id)
{
    var goloc = '';
    switch(id)
    {
        case 'inf': goloc = 'main.php?get_id=56&act=10&go=inf&vcode='+bavail[id][0]; break;
        case 'inv': goloc = 'main.php?get_id=56&act=10&go=inv&vcode='+bavail[id][0]; break;
        case 'ogl': Ogl(bavail[id][0]); break;
        case 'fis': Fish(bavail[id][0]); break;
        case 'fig': fight_map(bavail[id][0]); break;
        case 'dep': goloc = 'main.php?get_id=56&act=10&go=dep&vcode='+bavail[id][0]; break;
        case 'dri': Drink(bavail[id][0]); break;
        case 'dig': Digg(bavail[id][0]); break;
        case 'que': QActive(bavail[id][0]); break;   
    }
    if(goloc)
    {
        for(var j=0; j<bavail[id][1].length; j++) goloc += '&'+bavail[id][1][j][0]+'='+bavail[id][1][j][1];
        location = goloc;
    }    
}

function ButtonSt(st)
{
    for(var i=0; i<mapbt.length; i++)
    {
        d.getElementById(mapbt[i][0]).disabled = st;    
    }    
}

function ReInitBut(obj)
{
    for(var i=0; i<obj.length; i++) bavail[obj[i][0]] = [obj[i][2],obj[i][3]]; 
}

function ReAddBut(obj)
{
    var k = mapbt.length; 
    for(var i=0; i<obj.length; i++)
    {
        var nbutt = d.getElementById(obj[i][0]);
        if(!nbutt) 
        {
            mapbt[k] = [obj[i][0]];
            k++;
            bavail[obj[i][0]] = [obj[i][2],obj[i][3]];
            d.getElementById('ButtonPlace').innerHTML += ' <input type=button class=fr_but id="'+obj[i][0]+'" value="'+obj[i][1]+'" onclick=\'ButClick("'+obj[i][0]+'")\'>';
        } 
    }        
}

function showMap(x, y)
{
    var table, tbody, tr, td, img;

    if(!world) 
    {
        world = d.createElement('DIV');
        world.id = 'world_map';
        d.getElementById('world_cont').appendChild(world);  
    }
    world.innerHTML = '';
    table = d.createElement('TABLE');
    world.appendChild(table);
    tbody = d.createElement('TBODY');
    table.appendChild(tbody);
    table.border = 0;
    table.cellPadding = 0;
    table.cellSpacing = 0;
    
    for(var i = -height; i <= height; i++)
    {
        tr = d.createElement('TR');
        for(var j = -width; j <= width; j++)
        {
            td = d.createElement('TD');
            td.style.backgroundImage = 'url(http://image.neverlands.ru/map/world/'+map[0][3]+'/'+(y+i)+'/'+(x+j)+'_'+(y+i)+'.jpg)';
            
            img = d.createElement('IMG');
            img.src = 'http://image.neverlands.ru/1x1.gif';
            img.width = 100;
            img.height = 100;
            img.id = 'img_'+(x+j)+'_'+(y+i);
            
            dx = x+j;
            dy = y+i;
            
            if(avail[dx+'_'+dy] && !finStatus) 
            {
                img.src = 'http://image.neverlands.ru/map/world/here.gif';
                img.onclick = function(dx, dy) { return function() { moveMapTo(dx, dy, map[0][2]); } }(dx, dy);
                img.style.cursor = 'pointer';
            }
            
            td.appendChild(img);
            tr.appendChild(td);
        }
        tbody.appendChild(tr);
    }
    
    current_x = x;
    current_y = y;
    
    loaded_left = x-width;
    loaded_right = x+width;
    loaded_top = y-height;
    loaded_bottom = y+height;
    
    return true;
}

function finFunction()
{
    moving_status = 0;
    switch(finStatus)
    {
        case 0:
        
        current_x = parseInt(arr_res[1]);
        current_y = parseInt(arr_res[2]);
        var objmap = eval(arr_res[5]);
        map[0][2] = objmap[0];
        map[0][3] = objmap[1];
        map[1] = eval(arr_res[3]);
        MapReInit(map[1]);
        mapbt = eval(arr_res[4]);
        d.getElementById('ButtonPlace').innerHTML = ButtonGen();
        if(objmap[2]) MessBoxDiv(objmap[2]);
        
        break;
        case 1:
        
        finStatus = 0;
        current_x = map[0][0];
        current_y = map[0][1];
        ButtonSt(false);
        MapReInit(map[1]);

        break;    
    }
    
    if(pngAlpha) transport_img.src = 'http://image.neverlands.ru/map/nl_cursor.png';
    else 
    {
        transport_img = ReInitCursor();
        transport_img.src = 'http://image.neverlands.ru/map/nl_cursor.png';
    }
    
    parent.frames["ch_list"].location = "/ch.php?lo=1";       
}

function MapReInit(obj)
{
    var i, j, imgid, dx, dy;
    avail = {};
    for(i=0; i<obj.length; i++)
    {
        avail[obj[i][0]+'_'+obj[i][1]] = obj[i][2];
    }
    
    for(i = -height; i <= height; i++)
    {
        for(j = -width; j <= width; j++)
        {
            imgid = d.getElementById('img_'+(current_x+j)+'_'+(current_y+i));

            dx = current_x + j;
            dy = current_y + i;
            
            if(avail[dx+'_'+dy]) 
            {
                imgid.src = 'http://image.neverlands.ru/map/world/here.gif';
                imgid.onclick = function(dx, dy) { return function() { moveMapTo(dx, dy, map[0][2]); } }(dx, dy);
                imgid.style.cursor = 'pointer';
            }
            else
            {
                imgid.src = 'http://image.neverlands.ru/1x1.gif';
                imgid.onclick = function() {};
                imgid.style.cursor = 'default';
            }
        }
    }    
}

function move()
{
    var app_y, app_x;
    var path = ((time_left) / (pause * 1000));
    
    if(time_left <= 0) 
    {
        clearInterval(t);
        finFunction();
    }
    
    if(dest_y < current_y)
    {
        app_y = dest_y + (Math.abs(dest_y - current_y) * path);
        if((app_y - height) <= (loaded_top + 0.2)) 
        {
            loaded_top -= 1;
            loadMap('top', loaded_top);
        }
        
        if((app_y + (height*2)) <= (loaded_bottom)) 
        {
            loaded_bottom -= 1;
            freeMap('bottom');
        }
        
        cur_margin_top += (Math.abs(dest_y - current_y) * 100) / (pause*1000 / move_interval);
    } 
    else if(dest_y > current_y)
    {
        app_y = dest_y - (Math.abs(dest_y - current_y) * path);
        if((app_y + height) >= (loaded_bottom - 0.2)) 
        {
            loaded_bottom += 1;
            loadMap('bottom', loaded_bottom);
        }
        
        if((app_y - (height*2)) >= (loaded_top)) 
        {
            loaded_top += 1;
            freeMap('top');
        }
        
        cur_margin_top -= (Math.abs(dest_y - current_y) * 100) / (pause*1000 / move_interval);
    }
    
    if(dest_x < current_x)
    {
        app_x = dest_x + (Math.abs(dest_x - current_x) * path);
        if((app_x - width) <= (loaded_left + 0.2)) 
        {
            loaded_left -= 1;
            loadMap('left', loaded_left);
        }
        
        if((app_x + (width*2)) <= (loaded_right)) 
        {
            loaded_right -= 1;
            freeMap('right');
        }
        
        cur_margin_left += (Math.abs(dest_x - current_x) * 100) / (pause*1000 / move_interval);
    } 
    else if(dest_x > current_x)
    {
        app_x = dest_x - (Math.abs(dest_x - current_x) * path);
        if((app_x + width) >= (loaded_right - 0.2)) 
        {
            loaded_right += 1;
            loadMap('right', loaded_right);
        }
        
        if((app_x - (width*2)) >= (loaded_left)) 
        {
            loaded_left += 1;
            freeMap('left');
        }
        
        cur_margin_left -= (Math.abs(dest_x - current_x) * 100) / (pause*1000 / move_interval);
    }
    
    world.style.marginTop = parseInt(cur_margin_top) + 'px';
    world.style.marginLeft = parseInt(cur_margin_left) + 'px';
    
    time_left -= move_interval;
}

function timerst(lp)
{
    time_left_sec -= 1000;
    if(time_left_sec <= 0)
    {
        if(lp)
        {
            ButtonSt(false);
            MapReInit(map[1]);
            finStatus = 0;
        }
        timer_img.src = 'http://image.neverlands.ru/1x1.gif';
        d.getElementById('tdsec').innerHTML = '';
        d.getElementById('timerdiv').style.display = 'none';
        d.getElementById('timerfon').style.display = 'none';
        clearInterval(tsec);  
    }
    else
    {
        d.getElementById('tdsec').innerHTML = (time_left_sec / 1000); 
    }    
}

function RetClass()
{
    var userAgent = navigator.userAgent.toLowerCase();
    if(userAgent.indexOf('mac') != -1 && userAgent.indexOf('firefox')!=-1) classn = 'TB_overlayMacFFBGHack';
    else classn = 'TB_overlayBG';
    return classn;    
}

function StateReady()
{
    var messb;
    switch(arr_res[0])
    {
        case 'GO':
        
        MapReInit([]);
        
        //showTransport('dirizhopel', current_x, current_y, gox, goy, 16, 'png');
        showTransport('man', current_x, current_y, gox, goy, 8, 'gif');

        dest_x = gox;
        dest_y = goy;
        pause = gop;
        
        TimerStart(pause,0);
        time_left = pause*1000;
        moving_status = 1;

        ButtonSt(true);
        t = setInterval("move()", move_interval);
        
        break;
        case 'MESS':
        
        if(ND) RemoveDialogDiv();
        messb = eval(arr_res[1]);
        if(messb[2]) TimerStart(messb[2],1);
        MessBoxDiv(messb[0]);
        
        break;
        //case 'AL':
        case 'RESO':
        
        var n_map = eval(arr_res[2]);
        if(n_map[0] > 0) 
        {
            map[1] = n_map[1];
            map[0][2] = n_map[0];
            ReAddBut(eval(arr_res[3]));    
        }
        
        var dis_map = eval(arr_res[4]);
        /*
        if(!dis_map[0]) ReInitBut(eval(arr_res[3]));
        else
        {
            mapbt = eval(arr_res[3]);
            d.getElementById('ButtonPlace').innerHTML = ButtonGen();
            MapReInit([]);
        }*/
        mapbt = eval(arr_res[3]);
        d.getElementById('ButtonPlace').innerHTML = ButtonGen();
        if(dis_map[0]) MapReInit([]);
        //---
        
        if(dis_map[1][1]) TimerStart(dis_map[1][1],1);
        if(ND) RemoveDialogDiv();

        messb = eval(arr_res[1]);
        if(ND === false)
        {
            if(!messb[0])
            {
                ND = d.createElement('div');
                ND.id = 'darker';
                ND.className = (classn ? classn : RetClass());
                ND.style.display = 'block';
                d.body.appendChild(ND);
            
                ND = d.createElement('div');
                ND.className = 'png';
            
                // окно с данными                
                var buttons = '';
                var ingr = eval(arr_res[5]);
                var did = 'uni';
                ND.id = 'uni';
                
                switch(ingr[0])
                {
                    case 0:
                    
                    var tr = 0;
                    var butalt;
                    var messal = '<FORM id="ALHF"><table cellpadding=0 cellspacing=0 border=0 width=100%><tr><td bgcolor=#CCCCCC><table cellpadding=10 cellspacing=1 border=0 width=100%>'+(ingr[1] != '00000' ? '<tr><td bgcolor=#FFFFFF colspan=4 class="centr"><img src=http://image.neverlands.ru/1x1.gif width=1 height=10><br><img src="/modules/code/code.php?'+ingr[1]+'" width=134 height=60><br><img src=http://image.neverlands.ru/1x1.gif width=1 height=10><br>Код: <input type=text name=code size=4 class=gr_text id=CAPCODE><br><img src=http://image.neverlands.ru/1x1.gif width=1 height=10></td></tr>' : '');
                    
                    for(var i=4; i<ingr.length; i++)
                    {
                        tr++;
                        if(tr == 1) messal += '<tr>';
                        butalt = ingr[i][10] == 4 ? 'Срезать' : 'Срубить';
                        messal += '<td bgcolor=#FFFFFF valign=top width=25%><div align=center>'+(!ingr[i][9] ? '<input type=button class=lbutdis value="'+butalt+'" DISABLED>' : '<input type=button class=lbut value="'+butalt+'" onclick="ResoStart(\''+ingr[i][0]+'\','+ingr[2]+','+ingr[3]+',\''+ingr[i][3]+'\',\''+ingr[i][2]+'\',\''+ingr[i][4]+'\',\''+ingr[i][5]+'\',\''+ingr[i][6]+'\',\''+ingr[i][7]+'\',\''+ingr[i][9]+'\',\''+ingr[i][10]+'\')">')+'<br><br><img src=http://image.neverlands.ru/resources/'+ingr[i][0]+'.gif width=60 height=60><br><font class=freetxt><b>'+ingr[i][1]+'</b><br><br>Количество: '+ingr[i][8]+' из '+ingr[i][11]+'</font></div></td>';
                        
                        if(tr == 4) 
                        {
                            messal += '</tr>';
                            tr = 0;
                        }    
                    }
                    
                    tr++;
                    if(tr != 1)
                    {
                        for(var i=tr; i<5; i++) messal += '<td bgcolor=#FFFFFF width=25%>&nbsp;</td>';
                        messal += '</tr>';
                    }
                    
                    messal += '</table></td></tr></table></FORM>';
                    
                    buttons = '<a class="but ok" href="javascript: RemoveDialogDiv();"></a>';
                    
                    break;
                    case 1:
                    
                    var messal = '<FORM id="FISHF"><table cellspacing=0 cellpadding=0 border=0 width=100%><tr><td bgcolor=#CCCCCC><table cellspacing=1 cellpadding=5 border=0 width=100%><tr><td bgcolor=#FFFFFF colspan=5 class="centr" class=nickname><font class=inv><b>'+((ingr[4] - ingr[3]) > 10 ? '' : '<font color=#CC0000>Внимание! Возможен перегруз.</font> ')+'Масса Вашего инвентаря: '+ingr[3]+'/'+ingr[4]+'</b></font></td></tr><tr><td bgcolor=#FFFFFF colspan=2></td><td bgcolor=#FFFFFF class="centr" width=60%><b>Название приманки</b></td><td bgcolor=#FFFFFF class="centr" width=40%><b>В наличии</b></td></tr>';
                    
                    for(var i=5; i<ingr.length; i++) messal += '<tr><td bgcolor=#FFFFFF class="centr"><input type=radio name=primid value='+ingr[i][0]+(ingr[i][2] > 4 ? '' : ' DISABLED')+'></td><td bgcolor=#FFFFFF><img src=http://image.neverlands.ru/tools/'+ingr[i][0]+'.gif width=60 height=60></td><td bgcolor=#FFFFFF class="centr"><b>'+ingr[i][1]+'</b></td><td bgcolor=#FFFFFF class="centr"><b>'+ingr[i][2]+'</b></td></tr>';
                    
                    messal += (ingr[1] ? '<tr><td bgcolor=#FFFFFF colspan=5 class="centr"><img src=http://image.neverlands.ru/1x1.gif width=1 height=10><br><img src="/modules/code/code.php?'+ingr[1]+'" width=134 height=60><br><img src=http://image.neverlands.ru/1x1.gif width=1 height=10><br>Код: <input type=text name=code size=4 class=gr_text id=CAPCODE><br><img src=http://image.neverlands.ru/1x1.gif width=1 height=10></td></tr>' : '')+'</table></td></tr></table></FORM>';
                    
                    buttons = '<a class="but lov" href="javascript: FishStart(\''+ingr[2]+'\','+(ingr[1] ? 1 : 0)+');"></a>'; 
                    
                    break;
                }
                
                var mhtml = '<table width="760" cellspacing="0" cellpadding="0" border="0" class="uni_window"><tr><td class="wu_top_left png"></td><td class="wu_top"></td><td class="wu_top_right png"></td></tr><tr><td class="wu_l_gr"></td><td class="wu_m_gr">'+messal+'</td><td class="wu_r_gr"><a href="javascript: RemoveDialogDiv();" class="circ"></a></td> </tr><tr><td class="wu_b_l png"></td><td width="auto" class="wu_b_m"><table width="100%" cellspacing="0" cellpadding="0" border="0"><tr><td class="wu_b_m_l"></td><td>'+buttons+'</td><td class="wu_b_m_r"></td></tr></table></td><td class="wu_b_r png"></td></tr><tr><td colspan="3"><div class="wu_bb_l png"></div><div class="wu_bb_r png"></div></td></tr></table>'; 
                
                d.body.appendChild(ND);
            
                LD = d.getElementById(did);
                LD.innerHTML = mhtml; 
            
                DD = d.getElementById('darker');
                DD.style.height = getDocHeight()+'px';
             
            }
            else MessBoxDiv(messb[0]);
        }
        
        break;
        case 'F5':
        
        location = 'main.php';
        
        break;
    }    
}

function TimerStart(secgo,mrinit)
{
    if(time_left_sec <= 0)
    {
        if(mrinit)
        {
            ButtonSt(true);
            MapReInit([]);
        }
        time_left_sec = secgo*1000;
        if(!timer_img) createCursor();
        timer_img.src = 'http://image.neverlands.ru/map/world/timer.png';
        d.getElementById('timerfon').style.display = 'block';
        d.getElementById('timerdiv').style.display = 'block';
        d.getElementById('tdsec').innerHTML = secgo;
        tsec = setInterval('timerst('+mrinit+')', 1000);
    }
    else time_left_sec += secgo*1000;     
}

function MessBoxDiv(mess)
{
    if(!MESSD)
    {
        MDARK = d.createElement('div');
        MDARK.id = 'darker';
        MDARK.className = (classn ? classn : RetClass());
        MDARK.style.display = 'block';
        d.body.appendChild(MDARK);
        
        MESSD = d.createElement('div');
        MESSD.className = 'png';
        MESSD.id = 'static_window';
        MESSD.innerHTML = '<div class="ws_top png"></div><div class="ws_right png"></div><div class="ws_bottom png"></div><div class="ws_middle"><a href="javascript: MessBoxDivClose();" class="circ"></a><div class="text">'+mess+'</div><a class="cl_but" href="javascript: MessBoxDivClose();"></a></div>';
        d.body.appendChild(MESSD);        
    }    
}

function MessBoxDivClose()
{
    d.body.removeChild(MESSD);
    d.body.removeChild(MDARK);
    MDARK = false;
    MESSD = false;    
}

function fight_map(vcode)
{
    top.frames['ch_buttons'].document.FBT.text.focus();
    MessBoxDiv('<form action=main.php method=POST><input type=hidden name=post_id value="8"><input type=hidden name=vcode value='+vcode+'><table cellpadding=5 cellspacing=0 border=0 width=100%><tr><td><b>Нападение на природе</b></td></tr><tr><td>На кого: <input type="text" name=pnick class=gr_text maxlength=20></td></tr><tr><td align=center><input type=submit value="Выполнить" class=gr_but></td></tr></table></FORM>');
    d.all('pnick').focus();
    ActionFormUse = 'pnick';
}

function AlhStart(ct,cid,uid,curs,mass,muid,p,resl,vcode)
{
    var CAP;
    var errm = '';
    CAP = d.getElementById("CAPCODE").value;
    if(CAP)
    {
        AjaxGet('alchemy_ajax.php?act=2&ct='+ct+'&cid='+cid+'&uid='+uid+'&curs='+curs+'&mass='+mass+'&muid='+muid+'&p='+p+'&resl='+resl+'&vcode='+vcode+'&code='+CAP+'&r='+Math.random());    
    }
    else errm = 'Введите защитный код.';
    if(errm) MessBoxDiv(errm);     
}

function ResoStart(res_id,r_x,r_y,r_time,l_time,uid,curs,mass,p,vcode,r_type)
{
    var CAP;
    var errm = '';
    CAP = d.getElementById("CAPCODE").value;
    if(CAP)
    {
        AjaxGet('alchemy_ajax.php?act=3&res_id='+res_id+'&r_x='+r_x+'&r_y='+r_y+'&r_time='+r_time+'&r_type='+r_type+'&uid='+uid+'&curs='+curs+'&mass='+mass+'&p='+p+'&l_time='+l_time+'&vcode='+vcode+'&code='+CAP+'&r='+Math.random());    
    }
    else errm = 'Введите защитный код.';
    if(errm) MessBoxDiv(errm);     
}

function FishStart(vcode,ver)
{
    var CAP;
    var errm = '';
    if(ver) CAP = d.getElementById("CAPCODE").value;
    else CAP = 1;
    
    if(CAP) 
    {
        var primid = '';
        var ff = d.getElementById("FISHF");
        var radio = ff.primid;
        if(radio.value) primid = radio.value;
        else
        {
            for(var i=0; i<radio.length; i++)
            {
                if(radio[i].checked)
                {
                    primid = radio[i].value;
                    break;    
                }
            }
        }
        if(primid)
        {
            AjaxGet('fish_ajax.php?act=2&primid='+primid+'&vcode='+vcode+(ver ? '&code='+CAP : '')+'&r='+Math.random());
        }
        else errm = 'Не выбрана приманка.';       
    }
    else errm = 'Введите защитный код.';
    if(errm) MessBoxDiv(errm);
}

function getDocHeight() 
{
    return Math.max(Math.max(d.body.scrollHeight,d.documentElement.scrollHeight),Math.max(d.body.offsetHeight,d.documentElement.offsetHeight),Math.max(d.body.clientHeight,d.documentElement.clientHeight));
}

function moveMapTo(x, y, ps)
{
    if(moving_status == 1) return false;
    gox = x;
    goy = y;
    gop = ps;
    AjaxGet('map_ajax.php?act=1&mx='+x+'&my='+y+'&gti='+map[0][2]+'&vcode='+avail[x+'_'+y]+'&r='+Math.random());
    return true;
}

function Ogl(code)
{
    AjaxGet('alchemy_ajax.php?act=1&vcode='+code+'&r='+Math.random());     
}

function Fish(code)
{
    AjaxGet('fish_ajax.php?act=1&vcode='+code+'&r='+Math.random());    
}

function Drink(code)
{ 
    AjaxGet('map_act_ajax.php?act=1&vcode='+code+'&sm='+(map[1].length ? 1 : 0)+'&r='+Math.random());    
}

function Digg(code)
{ 
    AjaxGet('map_act_ajax.php?act=2&vcode='+code+'&sm='+(map[1].length ? 1 : 0)+'&r='+Math.random());    
}

function loadMap(dir)
{
    var tbody = world.lastChild.lastChild;
    var tr, td, img, i;
    switch (dir) 
    {
        case 'bottom':
        
        tr = d.createElement('TR');
        for(i = loaded_left; i <= loaded_right; i++)
        {
            td = d.createElement('TD');
            td.style.backgroundImage = 'url(http://image.neverlands.ru/map/world/'+map[0][3]+'/'+(loaded_bottom)+'/'+(i)+'_'+(loaded_bottom)+'.jpg)';
            img = d.createElement('IMG');
            img.src = 'http://image.neverlands.ru/1x1.gif';
            img.width = 100;
            img.height = 100;
            img.id = 'img_'+(i)+'_'+(loaded_bottom);
            td.appendChild(img);
            tr.appendChild(td);
        }
        tbody.appendChild(tr);
        
        break;
        case 'top':
        
        cur_margin_top -= 100; 
        tr = d.createElement('TR');
        for(i = loaded_left; i <= loaded_right; i++)
        {
            td = d.createElement('TD');
            td.style.backgroundImage = 'url(http://image.neverlands.ru/map/world/'+map[0][3]+'/'+(loaded_top)+'/'+(i)+'_'+(loaded_top)+'.jpg)';
            img = d.createElement('IMG');
            img.src = 'http://image.neverlands.ru/1x1.gif';
            img.width = 100;
            img.height = 100;
            img.id = 'img_'+(i)+'_'+(loaded_top);
            td.appendChild(img);
            tr.appendChild(td);
        }
            
        tbody.insertBefore(tr, tbody.firstChild);
        
        break;
        case 'right':
        
        for(i = loaded_top; i <= loaded_bottom; i++)
        {
            tr = tbody.childNodes[i-loaded_top];
            td = d.createElement('TD');
            td.style.backgroundImage = 'url(http://image.neverlands.ru/map/world/'+map[0][3]+'/'+(i)+'/'+(loaded_right)+'_'+(i)+'.jpg)';
            img = d.createElement('IMG');
            img.src = 'http://image.neverlands.ru/1x1.gif';
            img.width = 100;
            img.height = 100;
            img.id = 'img_'+(loaded_right)+'_'+(i);
            td.appendChild(img);
            tr.appendChild(td);
        }
            
        break;
        case 'left':
        
        cur_margin_left -= 100;
        for(i = loaded_top; i <= loaded_bottom; i++)
        {
            tr = tbody.childNodes[i-loaded_top];
            td = d.createElement('TD');
            td.style.backgroundImage = 'url(http://image.neverlands.ru/map/world/'+map[0][3]+'/'+(i)+'/'+(loaded_left)+'_'+(i)+'.jpg)';
            img = d.createElement('IMG');
            img.src = 'http://image.neverlands.ru/1x1.gif';
            img.width = 100;
            img.height = 100;
            img.id = 'img_'+(loaded_left)+'_'+(i);
            td.appendChild(img);
            tr.insertBefore(td, tr.firstChild);
        }
        
        break;
    }
}

function freeMap(dir)
{
    var tbody = world.lastChild.lastChild;
    var tr, i;

    switch(dir) 
    {
        case 'top':

        cur_margin_top += 100; 
        tr = tbody.firstChild;
        tbody.removeChild(tr);
        
        break;
        case 'bottom':

        tr = tbody.lastChild;
        tbody.removeChild(tr);
        
        break;
        case 'left':

        cur_margin_left += 100; 
        for (i = loaded_top; i <= loaded_bottom; i++)
        {
            tr = tbody.childNodes[i-loaded_top];
            tr.removeChild(tr.firstChild);
        }
        
        break;
        case 'right':
        
        for (i = loaded_top; i <= loaded_bottom; i++)
        {
            tr = tbody.childNodes[i-loaded_top];
            tr.removeChild(tr.lastChild);
        }
        
        break;
    }
    
    return true;
}

function loadPath(from_x, from_y, to_x, to_y, ptime_all, ptime_left)
{
    if(moving_status == 1) return false;
    var path = ((ptime_all - ptime_left) / ptime_all);
    var app_x = from_x + ((to_x - from_x) * path);
    var app_y = from_y + ((to_y - from_y) * path);
    showMap( parseInt(app_x), parseInt(app_y) );
    
    if(to_x < from_x) 
    {
        loaded_right++;
        loadMap('right');
    }
        
    if(to_y < from_y) 
    {
        loaded_bottom++;
        loadMap('bottom');
    }
    
    current_x = app_x;
    current_y = app_y;
    dest_x = to_x;
    dest_y = to_y;
    
    cur_margin_left = -(Math.abs(parseInt(app_x) - app_x) * 100);
    cur_margin_top = -(Math.abs(parseInt(app_y) - app_y) * 100);
    
    pause = ptime_left;
    time_left = pause*1000;
    
    moving_status = 1;
    t = setInterval("move()", move_interval);
    return true;
}

function createCursor()
{
    var div = d.createElement('DIV');
    div.id = 'cursor';

    div.style.display = 'block';
    div.style.position = 'absolute';
    div.style.marginLeft = (1 + (width)*100) + 'px';
    div.style.marginTop = (1 + (height)*100) + 'px';
    
    transport_img = d.createElement('IMG');
    transport_img.width = 100;
    transport_img.height = 100;
    
    div.appendChild(transport_img);
    d.getElementById('world_cont2').appendChild(div); 
    
    div = d.createElement('DIV');
    div.id = 'timerfon';
    
    div.style.display = 'none';
    div.style.position = 'absolute';
    div.style.marginLeft = ((width)*100) + 'px';
    div.style.marginTop = ((height - 1)*100) + 'px';
    
    timer_img = d.createElement('IMG');
    timer_img.width = 100;
    timer_img.height = 100;
    
    div.appendChild(timer_img);
    d.getElementById('world_cont2').appendChild(div);
    
    div = d.createElement('DIV');
    div.id = 'timerdiv';

    div.style.display = 'none';
    div.style.position = 'absolute';
    div.style.marginLeft = ((width)*100) + 'px';
    div.style.marginTop = (42 + (height - 1)*100) + 'px';
    div.innerHTML = '<table cellpadding=0 cellspacing=0 border=0 width=100><tr><td align=center id="tdsec" class="timer_s"></td></tr></table>';
    
    d.getElementById('world_cont2').appendChild(div);
}

function showCursor()
{
    if(!transport_img)
    {
        createCursor();    
    }
    transport_img.src = 'http://image.neverlands.ru/map/nl_cursor.png'; 
}

function showTransport(name, from_x, from_y, to_x, to_y, p, type)
{
    if(!transport_img)
    {
        createCursor();    
    }
    
    var rad = Math.atan2((to_y - from_y), (to_x - from_x));
    
    var pi = 3.141592;
    var grad = Math.round(rad/pi*180 / (360 / p));
    if (grad == p) grad = 0;
    if (grad < 0) grad = p + grad;
    
    
    if(pngAlpha) transport_img.src = 'http://image.neverlands.ru/map/'+name+'_'+grad+'.'+type;
    else
    {
        transport_img = ReInitCursor();
        transport_img.src = 'http://image.neverlands.ru/map/'+name+'_'+grad+'.'+type;
    }
   
    return true;
}

function ReInitCursor()
{
    var new_tr = d.createElement('IMG');
    new_tr.width = 100;
    new_tr.height = 100;
    transport_img.parentNode.appendChild(new_tr);
    transport_img.parentNode.removeChild(transport_img);
    return new_tr;    
}