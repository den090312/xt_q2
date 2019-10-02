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
        for (let product_order of product_block) {
            
        }
    }
}