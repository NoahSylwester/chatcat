"use strict";
document.addEventListener("DOMContentLoaded", function(event) { 
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
var connectionID = '';
var groupName = '';
var invites = [];

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("InitUser", function (userID) {
    connectionID = userID;
    document.getElementById("connectionID").textContent = `Your connection ID: ${userID}`
});

connection.on("UsersOnline", function (userIDs) {
    $("#usersOnlineCount").text(userIDs.length)
    const usersOnline = $("#usersOnline");
    $(usersOnline).html('')
    for (let i = 0; i < userIDs.length; i++) {
        const userElement = $("<li></li>");
        $(userElement).text(userIDs[i]).addClass("userIDListElement");
        $(userElement).on("click", function(event) {
            $("#inviteConnectionID").text(userIDs[i])
            if (groupName === '') {
                alert("create a room first!")
            }
            else {
                $("#inviteRoomName").text(groupName)
                $("#inviteUserModal").modal('show');
            }
        })
        $(usersOnline).append(userElement)
    }
});

function updateInvites() {
    $('#invitesList').html('');
    for (let i = 0; i < invites.length; i++) {
        const invitedGroupName = invites[i][1];
        const fromUserID = invites[i][0];
        const row = $(`
        <tr>
            <td>${invitedGroupName}</td>
            <td>${fromUserID}</td>
            <td>
                <button class="accept catButton">accept</button>
            </td>
            <td>
                <button class="delete catButton">delete</button>
            </td>
        </tr>
        `)
        $(row).find(".accept").on("click", function(event) {
            event.preventDefault();
            connection.invoke("AddToGroup", invitedGroupName).catch(function (err) {
                return console.error(err.toString());
            });
            invites.splice(invites.indexOf([fromUserID, invitedGroupName]),1);
            updateInvites()
            $("#seeInvitesModal").modal('hide')
        })
        $(row).find(".delete").on("click", function(event) {
            invites.splice(invites.indexOf([fromUserID, invitedGroupName]),1);
            updateInvites()
        })
        $('#invitesList').append(row)
    }
    $('#invitesCount').text(invites.length)
}

connection.on("ReceiveInvite", function (fromUserID, invitedGroupName) {
    invites.push([fromUserID, invitedGroupName]);
    const row = $(`
    <tr>
        <td>${invitedGroupName}</td>
        <td>${fromUserID}</td>
        <td>
            <button class="accept catButton">accept</button>
        </td>
        <td>
            <button class="delete catButton">delete</button>
        </td>
    </tr>
    `)
    $(row).find(".accept").on("click", function(event) {
        event.preventDefault();
        connection.invoke("AddToGroup", invitedGroupName).catch(function (err) {
            return console.error(err.toString());
        });
        invites.splice(invites.indexOf([fromUserID, invitedGroupName]),1);
        updateInvites()
        $("#seeInvitesModal").modal('hide')
    })
    $(row).find(".delete").on("click", function(event) {
        invites.splice(invites.indexOf([fromUserID, invitedGroupName]),1);
        updateInvites()
    })
    $('#invitesList').append(row)
    $('#invitesCount').text(invites.length)
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
        connection.invoke("SendMessage", message, groupName).catch(function (err) {
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
            connection.invoke("SendMessage", message, groupName).catch(function (err) {
                return console.error(err.toString());
            });
        }
    }
})

// $("#inviteUserModal").on("show.bs.modal", function(event) {
//     if (groupName === '') {
//         console.log('here')
//         $('#inviteUserModal').modal('toggle')
//     }
//     else {
//         console.log('shown')
//     }
// })

document.querySelector("#createRoomConfirmButton").addEventListener("click", function(event) {
    event.preventDefault();
    const createdGroupName = $("#createGroupNameInput").val();
    $("#createGroupNameInput").val('');
    groupName = createdGroupName;
    connection.invoke("AddToGroup", groupName).catch(function (err) {
        return console.error(err.toString());
    });
    $("#groupNameDisplay").text(groupName);
    $('#createRoomModal').modal('hide');
})

document.querySelector("#inviteUserConfirmButton").addEventListener("click", function(event) {
    event.preventDefault();
    const invitedConnectionID = $("#inviteConnectionID").text();
    connection.invoke("SendGroupInvite", invitedConnectionID, groupName).catch(function (err) {
        return console.error(err.toString());
    });
    $('#inviteUserModal').modal('hide');
})

document.querySelector("#inviteDirectConfirmButton").addEventListener("click", function(event) {
    event.preventDefault();
    const invitedConnectionID = $("#inviteDirectInput").val();
    if (invitedConnectionID !== "") {
        if (groupName === '') {
            $("#errorDirectInvite").text("create a room first");
        }
        else {
            connection.invoke("SendGroupInvite", invitedConnectionID, groupName).catch(function (err) {
                return console.error(err.toString());
            });
            $("#errorDirectInvite").text('');
            $("#inviteDirectInput").val('');
            $('#inviteDirectModal').modal('hide');
        }
    }
    else {
        $("#errorDirectInvite").text("enter an ID");
    }
})

document.querySelector("#refreshButton").addEventListener("click", function(event) {
    event.preventDefault();
    connection.invoke("QueryUsersOnline").catch(function (err) {
        return console.error(err.toString());
    });
})

function handleConnectionIDChange(newConnectionID) {
    connectionID = newConnectionID;
}
});