window.onload = function () {
    logOut();

    setChosenProduct();
    setChosenProductDelete();
    setChosenCustomerOrders();
    setChosenManagerAdd();

    validateLogIn();
    validateRegister(); 
    validateAddProduct();
    validateAddOrders();
    validateChangePassword();
    validateManagerAdd();
    validateFeedback();

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

    function setChosenProduct() {
        var product_block = document.getElementsByClassName("product_block")[0];
        if (product_block) {
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
    }

    function setChosenProductDelete() {
        var product_block_delete = document.getElementsByClassName("product_block_delete")[0];
        if (product_block_delete) {
            product_block_delete.onclick = function (event) {
                var eventTarget = event.target;
                var form = eventTarget.parentElement;
                var divProduct = form.getElementsByClassName("product")[0];
                var productIdDelete = divProduct.getElementsByClassName("product_id_delete")[0];
                var chosenProductIdDelete = form.getElementsByClassName("chosenProductIdDelete")[0];
                chosenProductIdDelete.value = productIdDelete.value;
            }
        }
    }

    function setChosenCustomerOrders() {
        var customer_orders = document.getElementsByClassName("customer_orders")[0];
        if (customer_orders) {
            customer_orders.onclick = function (event) {
                var eventTarget = event.target;
                var tableRow = eventTarget.parentElement.parentElement;
                var tableCellOrderId = tableRow.getElementsByClassName("tableCellOrderId")[0];
                if (eventTarget.className == "cancelOrder") {
                    var orderCancelId = customer_orders.getElementsByClassName("orderCancelId")[0];
                    orderCancelId.value = tableCellOrderId.value;
                }
                if (eventTarget.className == "restoreOrder") {
                    var orderRestoreId = customer_orders.getElementsByClassName("orderRestoreId")[0];
                    orderRestoreId.value = tableCellOrderId.value;
                }
                if (eventTarget.className == "inWorkOrder") {
                    var inWorkOrderId = customer_orders.getElementsByClassName("inWorkOrderId")[0];
                    inWorkOrderId.value = tableCellOrderId.value;
                }
                if (eventTarget.className == "completeOrder") {
                    var orderCompleteId = customer_orders.getElementsByClassName("orderCompleteId")[0];
                    orderCompleteId.value = tableCellOrderId.value;
                }
            }
        }
    } 

    function validateLogIn() {
        var log_in = document.getElementsByClassName("log_in")[0];
        if (log_in) {
            log_in.onclick = function (event) {
                var user_log_in = log_in.parentElement;
                var alertbox = user_log_in.getElementsByClassName("alertbox")[0];
                alertbox.style.display = "none";
                var log_name = user_log_in.getElementsByClassName("log_name")[0];
                var log_pass = user_log_in.getElementsByClassName("log_pass")[0];
                if (!log_name.value) {
                    alertbox.style.display = "block";
                    alertbox.innerHTML = "Введите логин!";
                    event.preventDefault();
                    return;
                }
                if (!log_pass.value) {
                    alertbox.style.display = "block";
                    alertbox.innerHTML = "Введите пароль!";
                    event.preventDefault();
                    return;
                }
            }
        }
    }

    function validateRegister() {
        var register = document.getElementsByClassName("register")[0];
        if (register) {
            register.onclick = function (event) {
                var user_register = register.parentElement.parentElement;
                var alertbox = user_register.getElementsByClassName("alertbox")[0];
                alertbox.style.display = "none";
                var reg_name = user_register.getElementsByClassName("reg_name")[0];
                var reg_pass = user_register.getElementsByClassName("reg_pass")[0];
                if (!reg_name.value) {
                    alertbox.style.display = "block";
                    alertbox.innerHTML = "Введите логин!";
                    event.preventDefault();
                    return;
                }
                if (!reg_pass.value) {
                    alertbox.style.display = "block";
                    alertbox.innerHTML = "Введите пароль!";
                    event.preventDefault();
                    return;
                }
            }
        }
    }

    function validateAddProduct() {
        var submit_product_add = document.getElementsByClassName("submit_product_add")[0];
        if (submit_product_add) {
            submit_product_add.onclick = function (event) {
                var product_add = submit_product_add.parentElement;
                var alertbox = product_add.getElementsByClassName("alertbox")[0];
                alertbox.style.display = "none";
                var product_new_name = product_add.getElementsByClassName("product_new_name")[0];
                var product_new_price = product_add.getElementsByClassName("product_new_price")[0];
                if (!product_new_name.value) {
                    alertbox.style.display = "block";
                    alertbox.innerHTML = "Введите наименование товара!";
                    event.preventDefault();
                    return;
                }
                if (!product_new_price.value) {
                    alertbox.style.display = "block";
                    alertbox.innerHTML = "Введите цену товара!";
                    event.preventDefault();
                    return;
                }
            }
        }
    }

    function validateAddOrders() {
        var submit_orders = document.getElementsByClassName("submit_order");
        var product_block = document.getElementsByClassName("product_block")[0];
        if (submit_orders) {
            for (let submit_order of submit_orders) {
                validateOrder(submit_order, product_block);
            }
        }

        function validateOrder(submit_order, product_block) {
            submit_order.onclick = function(event) {
                var product = submit_order.parentElement;

                cleanAlertBoxes(product_block);

                var alertbox = product.getElementsByClassName("alertbox")[0];
                var product_quantity_value = product.getElementsByClassName("product_quantity_value")[0];
                var order_adress_value = product.getElementsByClassName("order_adress_value")[0];
                if (!product_quantity_value.value) {
                    alertbox.style.display = "block";
                    alertbox.innerHTML = "Введите количество товара!";
                    event.preventDefault();
                    return;
                }
                if (!order_adress_value.value) {
                    alertbox.style.display = "block";
                    alertbox.innerHTML = "Введите адрес доставки товара!";
                    event.preventDefault();
                    return;
                }
            };
        }

        function cleanAlertBoxes(product_block) {
            var alertboxes = product_block.getElementsByClassName("alertbox");
            for (let alertbox of alertboxes) {
                alertbox.style.display = "none";
            }
        }
    }

    function validateChangePassword() {
        var submit_change_password = document.getElementsByClassName("submit_change_password")[0];
        if (submit_change_password) {
            submit_change_password.onclick = function (event) {
                var user_change_password = submit_change_password.parentElement.parentElement;
                var alertbox = user_change_password.getElementsByClassName("alertbox")[0];
                alertbox.style.display = "none";
                var old_pass = user_change_password.getElementsByClassName("old_pass")[0];
                var new_pass = user_change_password.getElementsByClassName("new_pass")[0];
                var new_pass_confirm = user_change_password.getElementsByClassName("new_pass_confirm")[0];
                if (!old_pass.value) {
                    alertbox.style.display = "block";
                    alertbox.innerHTML = "Введите старый пароль!";
                    event.preventDefault();
                    return;
                }
                if (!new_pass.value) {
                    alertbox.style.display = "block";
                    alertbox.innerHTML = "Введите новый пароль!";
                    event.preventDefault();
                    return;
                }
                if (!new_pass_confirm.value) {
                    alertbox.style.display = "block";
                    alertbox.innerHTML = "Подтвердите новый пароль!";
                    event.preventDefault();
                    return;
                }
                if (new_pass.value != new_pass_confirm.value) {
                    alertbox.style.display = "block";
                    alertbox.innerHTML = "Введенные пароли не совпадают!";
                    event.preventDefault();
                    return;
                }
            }
        }
    }

    function setChosenManagerAdd() {
        var manager_add = document.getElementsByClassName("manager_add")[0];
        if (manager_add) {
            manager_add.addEventListener("change", managerAddChange);
            function managerAddChange() {
                var selectRole = manager_add.getElementsByClassName("selectRole")[0];
                var selectRoleId = manager_add.getElementsByClassName("selectRoleId")[0];
                var selectRoleIdChosen = manager_add.getElementsByClassName("selectRoleIdChosen")[0];
                selectRoleId.selectedIndex = selectRole.selectedIndex;
                selectRoleIdChosen.value = selectRoleId[selectRoleId.selectedIndex].value;
                var selectRank = manager_add.getElementsByClassName("selectRank")[0];
                var selectRankChosen = manager_add.getElementsByClassName("selectRankChosen")[0];
                selectRankChosen.value = selectRank[selectRank.selectedIndex].value;
            }
        }
    }

    function validateManagerAdd() {
        var manager_add = document.getElementsByClassName("manager_add")[0];
        if (manager_add) {
            var submit_manager_add = manager_add.getElementsByClassName("submit_manager_add")[0];
            submit_manager_add.onclick = function (event) {
                var alertbox = manager_add.getElementsByClassName("alertbox")[0];
                alertbox.style.display = "none";
                var manager_new_login = manager_add.getElementsByClassName("manager_new_login")[0];
                var manager_new_pass = manager_add.getElementsByClassName("manager_new_pass")[0];
                var manager_new_name = manager_add.getElementsByClassName("manager_new_name")[0];
                if (!manager_new_login.value) {
                    alertbox.style.display = "block";
                    alertbox.innerHTML = "Введите логин!";
                    event.preventDefault();
                    return;
                }
                if (!manager_new_pass.value) {
                    alertbox.style.display = "block";
                    alertbox.innerHTML = "Введите пароль!";
                    event.preventDefault();
                    return;
                }
                if (!manager_new_name.value) {
                    alertbox.style.display = "block";
                    alertbox.innerHTML = "Введите имя!";
                    event.preventDefault();
                    return;
                }
            }
        }
    }

    function validateFeedback() {
        var feedback_add = document.getElementsByClassName("feedback_add")[0];
        if (feedback_add) {
            var submit_feedback_add = feedback_add.getElementsByClassName("submit_feedback_add")[0];
            submit_feedback_add.onclick = function (event) {
                var alertbox = feedback_add.getElementsByClassName("alertbox")[0];
                alertbox.style.display = "none";
                var feedback_name = feedback_add.getElementsByClassName("feedback_name")[0];
                var feedback_text = feedback_add.getElementsByClassName("feedback_text")[0];
                if (!feedback_name.value) {
                    alertbox.style.display = "block";
                    alertbox.innerHTML = "Введите имя!";
                    event.preventDefault();
                    return;
                }
                if (!feedback_text.value) {
                    alertbox.style.display = "block";
                    alertbox.innerHTML = "Введите отзыв!";
                    event.preventDefault();
                    return;
                }
            }
        }
    }
}