$(document).ready(function ()
{ 
    LoadInventoryRecords();

    $("#btnSave").click(function ()
    {
        highlightRequiredFields();

        $('#frmInventory').validate();

        if ($('#frmInventory').valid())
        {
            var inventoryModel = $("#frmInventory").serialize();

            $.ajax({
                type: "post",
                url: "inventory/save",
                data: inventoryModel,
                datatype: "json",
                contenttype: "application/json; charset=utf-8",
                success: function (response) {
                    if (response != null && response == "Success") {
                        clearFields();
                        LoadInventoryRecords();
                    }
                    else if (response != null && response == "InventoryAvailable")
                    {
                        alert("This inventory already taken. Please try another");
                        return;
                    }
                },
                error: function (xhr, status, error) {
                    alert("Result: " + status + " " + error + " " + xhr.status + " " + xhr.statusText);
                }
            });
        }
    });

    function clearFields()
    {
        $("#txtName").val('');
        $("#txtDescription").val('');
        $("#txtPrice").val('');
    }

    function highlightRequiredFields()
    {
        var requiredFields = $('input.requrd');

        for (var counter = 0; counter < requiredFields.length; counter++)
        {
            if ($(requiredFields[counter]).val() == '') {
                $(requiredFields[counter]).addClass('highlight');
                $(requiredFields[counter-1]).focus();//name textbox
            }
            else 
                $(requiredFields[counter]).removeClass('highlight');
        }
    }

    function LoadInventoryRecords() {  
        var table = $('#gridInventory').DataTable();   

        if (table != null)
            table.destroy();

        $('#gridInventory').DataTable({
            "ajax":
            {
                "type": "get",
                "url": "inventory/getall",
                "datatype" : "json",
            },
            autoWidth: false,
            "columns": [
                { "data": "Name", "name": "Name", "title": "Name", "orderable": true, "width": "3000px" },              
                {
                    "data": "Price", "name": "Price", "title": "Price", "orderable": true, 
                    "render": function (Price) {
                        return '$' + Price;
                    }  
                },
                {
                    "data": "InventoryId", "orderable": false, "render": function (data)
                    {
                        return '<a class = "view" title = "View Records" data-val = ' + data + ' style="margin-left:12px; cursor:pointer;" data-toggle="modal" data-target="#viewInventoryDetailModal"><i class="glyphicon glyphicon-info-sign"></i></a> <a class="del" data-val = ' + data + ' title = "Delete Records" style= "margin-left:12px; cursor:pointer;"> <i class="glyphicon glyphicon-trash"></i></a >';
                    }                   
                },
            ],
            'processing': true,
            "language": {
                "emptyTable": "No records available"
            },
        });
    }    

    $(document).on('click', '.view', function (e)
    {
        var inventoryId = parseInt($(this).data("val"));
        var url = 'Inventory/Get/' + inventoryId;        

        $.ajax({
            type: "post",
            url: url,            
            datatype: "json",
            contenttype: "application/json; charset=utf-8",
            success: function (response)
            {
                if (response != null)
                {
                    $("#spnName").text(response.Name);
                    $("#spnPrice").text(response.Price);  
                    $("#spnDescription").text(response.Description);
                }
            },
            error: function (xhr, status, error) {
                alert("Result: " + status + " " + error + " " + xhr.status + " " + xhr.statusText);
            }
        });
    });

    $(document).on('click', '.del', function (e)
    {
        if (confirm("Are you sure you want to delete the record?"))
        {
            var inventoryId = parseInt($(this).data("val"));
            var url = 'Inventory/delete/'+inventoryId;            

            $.ajax({
                type: "post",
                url: url,
                datatype: "json",
                contenttype: "application/json; charset=utf-8",
                success: function (response) {
                    if (response == "Success") {                        
                        LoadInventoryRecords();
                    }
                },
                error: function (xhr, status, error) {
                    alert("Result: " + status + " " + error + " " + xhr.status + " " + xhr.statusText);
                }
            });
        }
        else {
            return false;
        }        
    });    
});