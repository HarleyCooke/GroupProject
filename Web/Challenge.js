let players = {};
let Userteams = {};

function GetPlayers(){
    $.ajax({
        url: "https://ballstatsapi.azurewebsites.net/api/players",
        dataType: "JSON",
        async: false,
        crossDomain: true,
        success: function (data){
            players = data;
        }
    });
}

function TeamSelect(ind){
    var s = document.getElementById(`TeamPick${ind}`);
    var teamID = s.options[s.selectedIndex].value;

    $.ajax({
        url:`https://ballstatsapi.azurewebsites.net/api/Userteams/${teamID}`,
        Method: "GET",
        dataType: "json",
        crossDomain: true,
        success: function (data){
            TableFill(ind,data)            
        }
    });
}

function CalcAverage(col){
    var tot = 0;
    if (col != "PLAYERVALUE"){        
        for (var i = 1; i < 16; i++){
            tot = tot + parseInt($(`#${col}${i}`).html())
        }
    }
    else{
        for (var i = 1; i < 16; i++){
            tot = tot + parseInt($(`#${col}${i}`).html().substring(1))            
        }
    }
    return tot = (tot / 15).toFixed(2);
}

function TableFill(ind,data){
    $(`#UserTeam${ind}`).css('visibility','visible')
    for (var i = 1; i < 16; i++){
    $(`#UserTeam${ind}`).append(
        `<tr>
            <td id="NAME${i}">${players[data[`Player${i}`]]["NAME"]}</td>
            <td id="AGE${i}">${players[data[`Player${i}`]]["AGE"]}</td>
            <td id="Position${i}">${players[data[`Player${i}`]]["Position"]}</td>
            <td id="RAT${i}">${players[data[`Player${i}`]]["RAT"].toFixed(2)}</td>
            <td id="FG_${i}">${players[data[`Player${i}`]]["FG_"].toFixed(2)}%</td>
            <td id="FT_${i}">${players[data[`Player${i}`]]["FT_"].toFixed(2)}%</td>
            <td id="TP_${i}">${players[data[`Player${i}`]]["TP_"].toFixed(2)}%</td>
            <td id="AST${i}">${players[data[`Player${i}`]]["AST"].toFixed(2)}</td>
            <td id="REB${i}">${players[data[`Player${i}`]]["REB"].toFixed(2)}</td>
            <td id="BLK${i}">${players[data[`Player${i}`]]["BLK"].toFixed(2)}</td>
            <td id="STL${i}">${players[data[`Player${i}`]]["STL"].toFixed(2)}</td>
            <td id="W_L${i}">${players[data[`Player${i}`]]["W_L"].toFixed(2)}</td>
            <td id="PLAYERVALUE${i}">$${players[data[`Player${i}`]]["PLAYERVALUE"].toFixed(0)}</td>
        </tr>`
    );
    }
    $(`#UserTeam${ind}`).append(
        `<tr>
            <td><strong>Team Averages:</strong></td>
            <td>${CalcAverage("AGE")}</td>
            <td>-</td>
            <td>${CalcAverage("RAT")}</td>
            <td>${CalcAverage("FG_")}</td>
            <td>${CalcAverage("FT_")}</td>
            <td>${CalcAverage("TP_")}</td>
            <td>${CalcAverage("AST")}</td>
            <td>${CalcAverage("REB")}</td>
            <td>${CalcAverage("BLK")}</td>
            <td>${CalcAverage("STL")}</td>
            <td>${CalcAverage("W_L")}</td>
            <td>${CalcAverage("PLAYERVALUE")}</td>
        </tr>`
    );
}

$(document).ready(function (){
    GetPlayers();

    $.ajax({
        url: "https://ballstatsapi.azurewebsites.net/api/Userteams/",
        dataType: "JSON",
        async: false,
        crossDomain: true,
        success: function (teamdata){
            Userteams = teamdata;
        }
    });

    var ele = document.getElementById('TeamPick1');
            for (var i = 0; i < Userteams.length; i++) {
                ele.innerHTML = ele.innerHTML +
                    '<option value="' + Userteams[i]['ID'] + '">' + Userteams[i]['TeamName'] +  '</option>';
            }

    var ele2 = document.getElementById('TeamPick2');
            for (var i = 0; i < Userteams.length; i++) {
                ele2.innerHTML = ele2.innerHTML +
                    '<option value="' + Userteams[i]['ID'] + '">' + Userteams[i]['TeamName'] + '</option>';
            }         
});
