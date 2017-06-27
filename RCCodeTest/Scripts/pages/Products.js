(function ($) { })(jQuery);

$(document).ready(function () {
    // http://datatables.net

    $('#ProductResultsTbl').dataTable({
        "bJQueryUI": true,
        "paging": true,
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
});




