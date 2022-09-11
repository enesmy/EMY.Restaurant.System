function editRecord(recordID, userGroupCode, userGroupName, userGroupDetails) {
    document.getElementById('exampleModalLabel').textContent = 'Edit User Group';
    document.getElementById('UserGroupCode').value = userGroupCode;
    document.getElementById('UserGroupName').value = userGroupName;
    document.getElementById('ToolTipDetails').value = userGroupDetails;
    document.getElementById('recordID').value = recordID;
    $("#NewOrEditModal").modal();
}

function CreateRecord() {
    document.getElementById('exampleModalLabel').textContent = 'Create User Group';
    document.getElementById('recordID').value = 0;
    $("#NewOrEditModal").modal('show');
}

function Save() {
    const UserGroupCode = document.getElementById('UserGroupCode').value;
    const UserGroupName = document.getElementById('UserGroupName').value;
    const ToolTipDetails = document.getElementById('ToolTipDetails').value;
    const recordID = document.getElementById('recordID').value;

    $.ajax({
        url: "/Account/SaveUserGroup",
        type: "POST",
        data:
        {
            UserGroupID: recordID,
            UserGroupCode: UserGroupCode,
            UserGroupName: UserGroupName,
            UserGroupToolTip: ToolTipDetails
        },
        success: function (data) {
            $("#NewOrEditModal").modal('hide');

            if (recordID == 0) {
                var table = document.getElementById("tblRecords");
                var row = table.insertRow();
                var cell1 = row.insertCell(0);
                var cell2 = row.insertCell(1);
                var cell3 = row.insertCell(2);
                var cell4 = row.insertCell(3);
                var cell5 = row.insertCell(4);
                cell1.innerHTML = data.substring(data.lastIndexOf(':') + 1);
                cell2.innerHTML = UserGroupCode
                cell3.innerHTML = UserGroupName
                cell4.innerHTML = ToolTipDetails
                cell5.innerHTML = 'Refresh!!'
            }
            MessageBox.Show("Success!", data);
        }, error: function (xhr, ajaxOptions, thrownError) {
            $("#NewOrEditModal").modal('hide');
            MessageBox.Show("Error!", xhr.responseText);
        }
    });



}

function delrecord(id) {

    var deleteissue = function () {
        $.ajax({
            url: "/Account/RemoveUserGroup?UserGroupID=" + id,
            type: "POST",
            success: function (data) {
                MessageBox.Show("Success!", data);
                document.getElementById('row' + id).remove();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                MessageBox.Show("Error!", xhr.responseText);
            }
        });
    }

    MessageBox.AskAccept('Delete Record', 'Are you sure you want to delete?', deleteissue);

}

function setDefault(id) {

    var defaultissue = function () {
        $.ajax({
            url: "/Account/SetDefaultUserGroup",
            type: "POST",
            data:
            {
                UserGroupID: id
            },
            success: function (data) {
                location.reload();
            }, error: function (xhr, ajaxOptions, thrownError) {
                MessageBox.Show("Error!", xhr.responseText);
            }
        });
    }

    MessageBox.AskAccept('Set as default!', 'Are you sure you want to set default?', defaultissue);
}