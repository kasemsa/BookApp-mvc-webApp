// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function FillSubCategories(lstCategoryCtrl, lstSubcategoryId) {

    var lstSubcategories = $("#" + lstSubcategoryId);
    lstSubcategories.empty();

    var selectedCategory = lstCategoryCtrl.options[lstCategoryCtrl.selectedIndex].value;

    if (selectedCategory != null && selectedCategory != '') {

        $.getJSON("/Books/GetSubcategoryByCategory", { categoryId: selectedCategory },
            function(subCategory) {
            if (subCategory != null && !jQuery.isEmptyObject(subCategory)) {
                $.each(subCategory, function (index, subCategory) {

                    lstSubcategories.append($('<option/>',
                        {
                            value: subCategory.value,
                            text: subCategory.text

                        }));
                });
            };

        });
    }
    return;
}
