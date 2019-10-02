window.onload = function () {
    logOut();

    function logOut() {
        var user_log_out = document.getElementsByClassName("user_log_out")[0];
        if (user_log_out) {
            log_out = user_log_out.getElementsByClassName("log_out")[0];
            if (log_out) {
                log_out_post = user_log_out.getElementsByClassName("log_out_post")[0];
                if (log_out_post) {
                    log_out_post.value = "loggedOut";
                }
            }
        }
    }

    var product_block = document.getElementsByClassName("product_block")[0];
    product_block.onclick = function (event) {
        var product = event.target.parentElement;

        var product_id = product.getElementsByClassName("product_id")[0];

        var product_quantity = product.getElementsByClassName("product_quantity")[0]; 
        var product_quantity_value = product_quantity.getElementsByClassName("product_quantity_value")[0];

        var chosenProductId = product.getElementsByClassName("chosenProductId")[0]; 
        var chosenProductQuantity = product.getElementsByClassName("chosenProductQuantity")[0]; 

        chosenProductId.value = product_id.value;
        chosenProductQuantity.value = product_quantity_value.value;
    }
}