(function ($) { })(jQuery);

$(document).ready(function () {

    // http://datatables.net
    $('#ProductResultsTbl').dataTable({
        "bJQueryUI": true,
        "paging": true,
        "pagingType": "simple",
        "colReorder": true,
        "lengthMenu": [[10, 25, 50, 100, -1], [10, 25, 50, 100, "All"]],
        "processing": true,
        "bServerSide": true,
        "rowId": "ProductID",
        "sAjaxSource": "ProductDataAjaxHandler",
        "aoColumns": [
            { "mDataProp": "Name" },
            { "mDataProp": "ProductNumber" },
            { "mDataProp": "Color" },
            { "mDataProp": "SafetyStockLevel" },
            { "mDataProp": "ReorderPoint" },
            { "mDataProp": "StandardCost" },
            { "mDataProp": "ListPrice" },
        ]
    }).makeEditable({
            sAddNewRowButtonId: "btnAddNewProduct"
    });

    // Validate form.
    $("#formAddNewRow").validate({
        errorClass: "errorMsg",
        rules: {
            Name: "required",
            ProductNumber: "required",
            Color: "required",
            SafetyStockLevel: {
                required: true,
                number: true
            },
            ReorderPoint: {
                required: true,
                number: true
            },
            StandardCost: {
                required: true,
                number: true
            },
            ListPrice: {
                required: true,
                number: true
            }
        },
        messages: {
            Name: "Name is required.",
            ProductNumber: "The Product Number is required.",
            Color: "Color is required.", 
            SafetyStockLevel: {
                required: "The Safety Stock Level is required.",
                number: "The Safety Stock Level must be numeric."
           },
            ReorderPoint: {
                required: "The Reorder Point is required.",
                number: "The Reorder Point must be numeric."
            },
            StandardCost: {
                required: "The Standard Cost is required.",
                number: "The Standard Cost must be numeric."
            },
            ListPrice: {
                required: "The List Price is required.",
                number: "The List Price must be numeric."
            }
        }
    });
});




