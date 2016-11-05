var d = document;
var world = false;
var width = 1;
var height = 1;
var scale = 100;
var abcmapwidth = (((width * 2) + 1) * scale) + (width * 2) + 2;
var abcmapheight = (((height * 2) + 1) * scale) + (height * 2) + 2;
document.write("<div style=\"text-align:center\"><div style=\"display:inline-block;\">");
document.write("<div id=\"world_cont\" style=\"position: absolute; text-align: center; overflow: hidden; width:" + abcmapwidth + "px; height:" + abcmapheight + "px;\"></div>");
document.write("<div id=\"world_cont2\" style=\"width: " + abcmapwidth + "px; height: " + abcmapheight + "px; text-align: left;\"></div>");
document.write("</div></div>");

function showMap(x, y) {
    var table, tbody, tr, td;

    if (!world) {
        world = document.createElement("DIV");
        world.id = "world_map";
        document.getElementById("world_cont").appendChild(world);
    }

    world.innerHTML = "";

    table = document.createElement("TABLE");
    table.cellPadding = 0;
    table.cellSpacing = 0;
    table.bgColor = "black";
    table.border = "1px solid black";
    world.appendChild(table);

    tbody = document.createElement("TBODY");
    table.appendChild(tbody);

    for (var i = -height; i <= height; i++) {
        var dy = y + i;
        tr = d.createElement("TR");
        for (var j = -width; j <= width; j++) {
            var dx = x + j;

            td = d.createElement("TD");
            td.id = "td_" + dx + "_" + dy;
            td.style.backgroundImage = "url(http://image.neverlands.ru/map/world/" + "day" + "/" + dy + "/" + dx + "_" + dy + ".jpg)";
            td.style.backgroundRepeat = "no-repeat";
            td.style.backgroundPosition = "left top";
            td.style.borderWidth = "0";

            td.style.width = "100px";
            td.style.height = "100px";
            td.style.display = "inline-block";
            td.style.verticalAlign = "top";
            td.style.textAlign = "left";
            td.style.opacity = "0.8";
            td.style.filter = "alpha(opacity=80)";

            var isCellExists = window.external.IsCellExists(dx, dy);
            if (isCellExists) {
                var isframe = (i == 0) && (j == 0);
                td.onclick = function (dx, dy) { return function () { window.external.MoveTo(window.external.GenMoveLink(dx, dy)); }; }(dx, dy);
                td.title = window.external.CellAltText(dx, dy, scale);
                td.style.cursor = "pointer";
                td.onmouseover = function () { this.style.opacity = "1.0"; this.style.filter = "alpha(opacity=100)"; };
                td.onmouseout = function () { this.style.opacity = "0.8"; this.style.filter = "alpha(opacity=80)"; };
                td.innerHTML = window.external.CellDivText(dx, dy, scale, td.onclick, false, isframe);
            }
            else {
                td.style.opacity = "0.4";
                td.style.filter = "alpha(opacity=40)";
                td.onclick = function () { return false; };
                td.title = "";
                td.style.cursor = "default";
                td.onmouseover = function () { return false; };
                td.onmouseout = function () { return false; };
                td.innerHTML = "";
            }

            tr.appendChild(td);
        }

        tbody.appendChild(tr);
    }
}