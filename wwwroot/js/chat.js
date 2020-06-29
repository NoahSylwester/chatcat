"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
var connectionID = ''

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("InitUser", function (userID) {
    connectionID = userID;
    document.getElementById("connectionID").textContent = `Your connection ID: ${userID}`
});

connection.on("UsersOnline", function (userIDs) {
    $("#usersOnlineCount").text(userIDs.length)
    const usersOnline = $("#usersOnline");
    usersOnline.innerHTML = '';
    for (let i = 0; i < userIDs.length; i++) {
        const userElement = $("<li></li>");
        $(userElement).text(userIDs[i]).addClass("userIDListElement");
        $(usersOnline).append(userElement)
    }
});

connection.on("ReceiveMessage", function (message, fromUserID) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var li = $("<li></li>");
    var header = $("<p></p>");
    var body = $("<p></p>");
    $(header).addClass("chatMessageHeader");
    $(body).addClass("chatMessageBody");
    $(li).addClass('chatMessageItem');
    if (fromUserID !== connectionID) {
        $(li).addClass('remoteUserItem');
    }
    $(header).text(fromUserID);
    $(body).text(msg);
    $(li).append(header);
    $(li).append(body);
    $("#messagesList").append(li);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    // var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    if (message) {
        document.getElementById("messageInput").value = "";
        connection.invoke("SendMessage", message).catch(function (err) {
            return console.error(err.toString());
        });
    }
    event.preventDefault();
});

document.addEventListener("keyup", function(event) {
    const key = event.keyCode || event.which;

    if (key === 13) {
        var message = document.getElementById("messageInput").value;
        if (message) {
            document.getElementById("messageInput").value = "";
            connection.invoke("SendMessage", message).catch(function (err) {
                return console.error(err.toString());
            });
        }
    }
})
function handleConnectionIDChange(newConnectionID) {
    connectionID = newConnectionID;
}