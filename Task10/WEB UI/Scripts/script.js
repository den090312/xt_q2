window.onload = function () {
    var messagebox = document.getElementById("messagebox"); 
    var options = document.getElementsByClassName("options");
    var operations = document.getElementsByClassName("operations");
    if (options) {
        proceedToOptions(options, operations);
    }
    if (operations) {
        proceedToOperations(operations);
    }
    setSelectedUserGuid();
    setSelectedAwardGuid();

    setJoinGuidUser();
    setJoinGuidAward();

    setImageGuidUser();
    setImageGuidAward();

    logIn();
    logOut();

    function setStyleDisplayNone(elementName) {
        var element = document.getElementsByClassName(elementName)[0];
        if (element) {
            element.style.display = "none";
        }
    }

    goToRegistration = document.getElementsByClassName("goToRegistration")[0]; 
    if (goToRegistration) {
        goToRegistration.onclick = function () {
            user_register = document.getElementsByClassName("user_register")[0];
            if (user_register) {
                user_register.style.display = "block";
                var reg_name = user_register.getElementsByClassName("reg_name")[0];
                var reg_pass = user_register.getElementsByClassName("reg_pass")[0]; 
                var alertbox = user_register.getElementsByClassName("alertbox")[0];
                alertbox.style.display = "none";
                setStyleDisplayNone("user_log_in");
                setStyleDisplayNone("authentication");
                setStyleDisplayNone("menu");
                setStyleDisplayNone("currentUserName");
                setStyleDisplayNone("currentUserRole");
                validateRegistration(reg_name, alertbox, reg_pass);
            }
        }
    }

    function validateRegistration(reg_name, alertbox, reg_pass) {
        var register = user_register.getElementsByClassName("register")[0];
        register.onclick = function (event) {
            if (!reg_name.value) {
                alertbox.style.display = "block";
                alertbox.innerHTML = "Name is missing!";
                event.preventDefault();
                return;
            }
            if (!reg_pass.value) {
                alertbox.style.display = "block";
                alertbox.innerHTML = "Password is missing!";
                event.preventDefault();
                return;
            }
        };
    }

    function setSelectedUserGuid() {
        var user_chosen = document.getElementsByClassName("user_chosen")[0];
        if (user_chosen) {
            user_chosen.addEventListener("change", userChosenChange); 
            var user_chosen_guid = document.getElementsByClassName("user_chosen_guid")[0];
            var user_guid = document.getElementsByClassName("user_guid")[0];
            user_guid.value = user_chosen_guid[user_chosen_guid.selectedIndex].value;
            function userChosenChange() {
                user_chosen_guid.selectedIndex = user_chosen.selectedIndex;
                user_guid.value = user_chosen_guid[user_chosen_guid.selectedIndex].value;
            }
        }
    }

    function setSelectedAwardGuid() {
        var award_chosen = document.getElementsByClassName("award_chosen")[0];
        if (award_chosen) {
            award_chosen.addEventListener("change", awardChosenChange);
            var award_chosen_guid = document.getElementsByClassName("award_chosen_guid")[0];
            var award_guid = document.getElementsByClassName("award_guid")[0];
            award_guid.value = award_chosen_guid[award_chosen_guid.selectedIndex].value;
            function awardChosenChange() {
                award_chosen_guid.selectedIndex = award_chosen.selectedIndex;
                award_guid.value = award_chosen_guid[award_chosen_guid.selectedIndex].value;
            }
        }
    }

    function setJoinGuidUser() {
        var user_chosen_join = document.getElementsByClassName("user_chosen_join")[0];
        if (user_chosen_join) {
            user_chosen_join.addEventListener("change", userChosenJoinChange);
            var user_chosen_join_guid = document.getElementsByClassName("user_chosen_join_guid")[0];
            var user_guid_join = document.getElementsByClassName("user_guid_join")[0];
            user_guid_join.value = user_chosen_join_guid[user_chosen_join_guid.selectedIndex].value;
            function userChosenJoinChange() {
                user_chosen_join_guid.selectedIndex = user_chosen_join.selectedIndex;
                user_guid_join.value = user_chosen_join_guid[user_chosen_join_guid.selectedIndex].value;
            }
        }
    }

    function setJoinGuidAward() {
        var award_chosen_join = document.getElementsByClassName("award_chosen_join")[0];
        if (award_chosen_join) {
            award_chosen_join.addEventListener("change", awardChosenJoinChange);
            var award_chosen_join_guid = document.getElementsByClassName("award_chosen_join_guid")[0];
            var award_guid_join = document.getElementsByClassName("award_guid_join")[0];
            award_guid_join.value = award_chosen_join_guid[award_chosen_join_guid.selectedIndex].value;
            function awardChosenJoinChange() {
                award_chosen_join_guid.selectedIndex = award_chosen_join.selectedIndex;
                award_guid_join.value = award_chosen_join_guid[award_chosen_join_guid.selectedIndex].value;
            }
        }
    }

    function setImageGuidUser() {
        var user_chosen_image = document.getElementsByClassName("user_chosen_image")[0];
        if (user_chosen_image) {
            user_chosen_image.addEventListener("change", userChosenImageJoinChange);
            var user_chosen_image_guid = document.getElementsByClassName("user_chosen_image_guid")[0];
            var user_image_guid = document.getElementsByClassName("user_image_guid")[0];
            user_image_guid.value = user_chosen_image_guid[user_chosen_image_guid.selectedIndex].value;
            function userChosenImageJoinChange() {
                user_chosen_image_guid.selectedIndex = user_chosen_image.selectedIndex;
                user_image_guid.value = user_chosen_image_guid[user_chosen_image_guid.selectedIndex].value;
            }
        }
    }

    function setImageGuidAward() {
        var award_chosen_image = document.getElementsByClassName("award_chosen_image")[0];
        if (award_chosen_image) {
            award_chosen_image.addEventListener("change", awardChosenImageJoinChange);
            var award_chosen_image_guid = document.getElementsByClassName("award_chosen_image_guid")[0];
            var award_image_guid = document.getElementsByClassName("award_image_guid")[0];
            award_image_guid.value = award_chosen_image_guid[award_chosen_image_guid.selectedIndex].value;
            function awardChosenImageJoinChange() {
                award_chosen_image_guid.selectedIndex = award_chosen_image.selectedIndex;
                award_image_guid.value = award_chosen_image_guid[award_chosen_image_guid.selectedIndex].value;
            }
        }
    }

    function proceedToOptions(options, operations) {
        for (let option of options) {
            var buttons = option.getElementsByClassName("button");
            if (buttons) {
                proceedToButtonOnclick();
            }
        }

        function proceedToButtonOnclick() {
            for (let button of buttons) {
                if (button) {
                    buttonOnclick(button, operations);
                }
            }
        }
    }

    function proceedToOperations(operations) {
        for (let operation of operations) {
            if (operation) {
                operationOnClick(operation);
            }
        }
    }

    function operationOnClick(operation) {
        operation.onclick = function (event) {
            var main = document.getElementById("main");
            var target = event.target;
            if (target) {
                switch (target.className) {
                    case "cancel":
                        goToMain(main, operation);
                        break;
                    case "user_create":
                        userCreation(main, event, operation);
                        break;
                    case "user_delete":
                        goToMain(main, operation);
                        break;
                    case "award_create":
                        awardCreation(main, event, operation);
                        break;
                    case "award_delete":
                        if (!confirm("Награда будет удалена у всех пользователей. Вы уверены?")) {
                            document.getElementsByClassName("award_guid")[0].value = "";
                            goToMain(main, operation);
                        }
                        break;
                    case "join_user_award":
                        goToMain(main, operation);
                        break;
                    case "upload_user_image":
                        uploadUserImage(main, event, operation);
                        break;
                    case "upload_award_image":
                        uploadAwardImage(main, event, operation);
                        break;
                    case "saveWebusers":
                        saveWebusers(main, event, operation);
                        break;
                }
            }
        }

        function userCreation(main, e, operation) {
            var user_creation = document.getElementsByClassName("user_creation")[0];
            var alertbox = user_creation.getElementsByClassName("alertbox")[0];
            alertbox.style.display = "none";
            var inputname = user_creation.getElementsByClassName("name")[0];
            if (!inputname.value) {
                alertbox.style.display = "block";
                alertbox.innerHTML = "Name is missing!";
                e.preventDefault();
                return;
            }
            var dateOfBirth = user_creation.getElementsByClassName("dateOfBirth")[0];
            if (!dateOfBirth.value) {
                alertbox.style.display = "block";
                alertbox.innerHTML = "Date of birth is missing!";
                e.preventDefault();
                return;
            }
            else if (new Date(dateOfBirth.value) >= new Date()) {
                alertbox.style.display = "block";
                alertbox.innerHTML = "Date of birth can't be more than current date!";
                e.preventDefault();
                return;
            }
        }

        function awardCreation(main, e, operation) {
            var award_creation = document.getElementsByClassName("award_creation")[0];
            var alertbox = award_creation.getElementsByClassName("alertbox")[0];
            alertbox.style.display = "none";
            var inputtitle = award_creation.getElementsByClassName("title")[0];
            if (!inputtitle.value) {
                alertbox.style.display = "block";
                alertbox.innerHTML = "Title is missing!";
                e.preventDefault();
                return;
            }
        }

        function uploadUserImage(main, e, operation) {
            var user_image_upload = document.getElementsByClassName("user_image_upload")[0];
            var userImage = user_image_upload.getElementsByClassName("userImage")[0];
            var alertbox = user_image_upload.getElementsByClassName("alertbox")[0];
            alertbox.style.display = "none";
            if (userImage.value == "") {
                alertbox.style.display = "block";
                alertbox.innerHTML = "File is missing!";
                e.preventDefault();
                return;
            }
        }

        function uploadAwardImage(main, e, operation) {
            var award_image_upload = document.getElementsByClassName("award_image_upload")[0];
            var awardImage = award_image_upload.getElementsByClassName("awardImage")[0];
            var alertbox = award_image_upload.getElementsByClassName("alertbox")[0];
            alertbox.style.display = "none";
            if (awardImage.value == "") {
                alertbox.style.display = "block";
                alertbox.innerHTML = "File is missing!";
                e.preventDefault();
                return;
            }
        }

        function saveWebusers(main, e, operation) {
            var adminpanel = document.getElementsByClassName("adminpanel")[0];
            var userRoleOutputs = document.getElementsByClassName("userRole");
            var adminCount = 0;
            var checkedRoles = [];
            for (let userRole of userRoleOutputs) {
                if (userRole.checked) {
                    checkedRoles.push(userRole.value);
                }
                if (userRole.checked & userRole.value.toLowerCase() == "admin") {
                    adminCount++;
                }
            }
            var alertbox = adminpanel.getElementsByClassName("alertbox")[0];
            alertbox.style.display = "none";
            if (adminCount == 0) {
                alertbox.style.display = "block";
                alertbox.innerHTML = "Atleast one admin must be!";
                e.preventDefault();
                return;
            }
            inputsForRoles = document.getElementsByClassName("checkedRole");
            for (var i = 0; i < inputsForRoles.length; i++) {
                inputsForRoles[i].value = checkedRoles[i]; 
            }
        }
    }

    function buttonOnclick(button, operations) {
        button.onclick = function (event) {
            var main = document.getElementById("main");
            var target = event.target;
            if (target) {
                switch (target.className) {
                    case "user_create":
                        displayUserCreation(main);
                        break;
                    case "user_delete":
                        displayUserDel(main);
                        break;
                    case "user_print":
                        displayListUsers();
                        break;
                    case "award_create":
                        displayAwardCreation(main);
                        break;
                    case "award_delete":
                        displayAwardDel(main);
                        break;
                    case "award_print":
                        displayListAwards(main);
                        break;
                    case "join_button":
                        displayJoin(main);
                        break;
                    case "user_edit":
                        displayUserEdition(main);
                        break;
                    case "award_edit":
                        displayAwardEdition(main);
                        break;
                    case "user_upload_image":
                        displayUserImageUpload(main);
                        break;
                    case "award_upload_image":
                        displayAwardImageUpload(main);
                        break;
                    case "user_administration":
                        displayAdminpanel(main);
                        break;
                }
            }
        };

        function displayUserDel(main) {
            main.style.display = "none";
            operations[0].style.display = "block";
            document.getElementsByClassName("user_del")[0].style.display = "block";
            setStyleDisplayNone("award_del");
            setStyleDisplayNone("user_creation");
            setStyleDisplayNone("award_creation");
            setStyleDisplayNone("listusers");
            setStyleDisplayNone("listawards");
            setStyleDisplayNone("join");
            setStyleDisplayNone("user_edition");
            setStyleDisplayNone("award_edition");
            setStyleDisplayNone("user_image_upload");
            setStyleDisplayNone("award_image_upload");
            setStyleDisplayNone("adminpanel");
        }

        function displayAwardDel(main) {
            main.style.display = "none";
            operations[0].style.display = "block";
            setStyleDisplayNone("user_del");
            document.getElementsByClassName("award_del")[0].style.display = "block";
            setStyleDisplayNone("user_creation");
            setStyleDisplayNone("award_creation");
            setStyleDisplayNone("listusers");
            setStyleDisplayNone("listawards");
            setStyleDisplayNone("join");
            setStyleDisplayNone("user_edition");
            setStyleDisplayNone("award_edition");
            setStyleDisplayNone("user_image_upload");
            setStyleDisplayNone("award_image_upload");
            setStyleDisplayNone("adminpanel");
        }

        function displayListAwards(main) {
            main.style.display = "none";
            operations[0].style.display = "block";
            setStyleDisplayNone("user_del");
            setStyleDisplayNone("award_del");
            setStyleDisplayNone("user_creation");
            setStyleDisplayNone("award_creation");
            setStyleDisplayNone("listusers");
            document.getElementsByClassName("listawards")[0].style.display = "block";
            setStyleDisplayNone("join");
            setStyleDisplayNone("user_edition");
            setStyleDisplayNone("award_edition");
            setStyleDisplayNone("user_image_upload");
            setStyleDisplayNone("award_image_upload");
            setStyleDisplayNone("adminpanel");
        }

        function displayListUsers() {
            main.style.display = "none";
            operations[0].style.display = "block";
            setStyleDisplayNone("user_del");
            setStyleDisplayNone("award_del");
            setStyleDisplayNone("user_creation");
            setStyleDisplayNone("award_creation");
            document.getElementsByClassName("listusers")[0].style.display = "block";
            setStyleDisplayNone("listawards");
            setStyleDisplayNone("join");
            setStyleDisplayNone("user_edition");
            setStyleDisplayNone("award_edition");
            setStyleDisplayNone("user_image_upload");
            setStyleDisplayNone("award_image_upload");
            setStyleDisplayNone("adminpanel");
        }

        function displayUserCreation(main) {
            main.style.display = "none";
            operations[0].style.display = "block";
            setStyleDisplayNone("user_del");
            setStyleDisplayNone("award_del");
            document.getElementsByClassName("user_creation")[0].style.display = "block";
            setStyleDisplayNone("award_creation");
            setStyleDisplayNone("listusers");
            setStyleDisplayNone("listawards");
            setStyleDisplayNone("join");
            setStyleDisplayNone("user_edition");
            setStyleDisplayNone("award_edition");
            setStyleDisplayNone("user_image_upload");
            setStyleDisplayNone("award_image_upload");
            setStyleDisplayNone("adminpanel");
        }

        function displayAwardCreation(main) {
            main.style.display = "none";
            operations[0].style.display = "block";
            setStyleDisplayNone("user_del");
            setStyleDisplayNone("award_del");
            setStyleDisplayNone("user_creation");
            document.getElementsByClassName("award_creation")[0].style.display = "block";
            setStyleDisplayNone("listusers");
            setStyleDisplayNone("listawards");
            setStyleDisplayNone("join");
            setStyleDisplayNone("user_edition");
            setStyleDisplayNone("award_edition");
            setStyleDisplayNone("user_image_upload");
            setStyleDisplayNone("award_image_upload");
            setStyleDisplayNone("adminpanel");
        }

        function displayJoin(main) {
            main.style.display = "none";
            operations[0].style.display = "block";
            setStyleDisplayNone("user_del");
            setStyleDisplayNone("award_del");
            setStyleDisplayNone("user_creation");
            setStyleDisplayNone("award_creation");
            setStyleDisplayNone("listusers");
            setStyleDisplayNone("listawards");
            document.getElementsByClassName("join")[0].style.display = "block";
            setStyleDisplayNone("user_edition");
            setStyleDisplayNone("award_edition");
            setStyleDisplayNone("user_image_upload");
            setStyleDisplayNone("award_image_upload");
            setStyleDisplayNone("adminpanel");
        }

        function displayUserEdition(main) {
            main.style.display = "none";
            operations[0].style.display = "block";
            setStyleDisplayNone("user_del");
            setStyleDisplayNone("award_del");
            setStyleDisplayNone("user_creation");
            setStyleDisplayNone("award_creation");
            setStyleDisplayNone("listusers");
            setStyleDisplayNone("listawards");
            setStyleDisplayNone("join");
            document.getElementsByClassName("user_edition")[0].style.display = "block";
            setStyleDisplayNone("award_edition");
            setStyleDisplayNone("user_image_upload");
            setStyleDisplayNone("award_image_upload");
            setStyleDisplayNone("adminpanel");
        }

        function displayAwardEdition(main) {
            main.style.display = "none";
            operations[0].style.display = "block";
            setStyleDisplayNone("user_del");
            setStyleDisplayNone("award_del");
            setStyleDisplayNone("user_creation");
            setStyleDisplayNone("award_creation");
            setStyleDisplayNone("listusers");
            setStyleDisplayNone("listawards");
            setStyleDisplayNone("join");
            setStyleDisplayNone("user_edition");
            document.getElementsByClassName("award_edition")[0].style.display = "block";
            setStyleDisplayNone("user_image_upload");
            setStyleDisplayNone("award_image_upload");
            setStyleDisplayNone("adminpanel");
        }

        function displayUserImageUpload(main) {
            main.style.display = "none";
            operations[0].style.display = "block";
            setStyleDisplayNone("user_del");
            setStyleDisplayNone("award_del");
            setStyleDisplayNone("user_creation");
            setStyleDisplayNone("award_creation");
            setStyleDisplayNone("listusers");
            setStyleDisplayNone("listawards");
            setStyleDisplayNone("join");
            setStyleDisplayNone("user_edition");
            setStyleDisplayNone("award_edition");
            document.getElementsByClassName("user_image_upload")[0].style.display = "block";
            setStyleDisplayNone("award_image_upload");
            setStyleDisplayNone("adminpanel");
        }

        function displayAwardImageUpload(main) {
            main.style.display = "none";
            operations[0].style.display = "block";
            setStyleDisplayNone("user_del");
            setStyleDisplayNone("award_del");
            setStyleDisplayNone("user_creation");
            setStyleDisplayNone("award_creation");
            setStyleDisplayNone("listusers");
            setStyleDisplayNone("listawards");
            setStyleDisplayNone("join");
            setStyleDisplayNone("user_edition");
            setStyleDisplayNone("award_edition");
            setStyleDisplayNone("user_image_upload");
            document.getElementsByClassName("award_image_upload")[0].style.display = "block";
            setStyleDisplayNone("adminpanel");
        }

        function displayAdminpanel(main) {
            main.style.display = "none";
            operations[0].style.display = "block";
            setStyleDisplayNone("user_del");
            setStyleDisplayNone("award_del");
            setStyleDisplayNone("user_creation");
            setStyleDisplayNone("award_creation");
            setStyleDisplayNone("listusers");
            setStyleDisplayNone("listawards");
            setStyleDisplayNone("join");
            setStyleDisplayNone("user_edition");
            setStyleDisplayNone("award_edition");
            setStyleDisplayNone("user_image_upload");
            setStyleDisplayNone("award_image_upload");
            document.getElementsByClassName("adminpanel")[0].style.display = "block";
        }
    }

    function goToMain(main, operation) {
        operation.style.display = "none";
        main.style.display = "block";
        messagebox.style.display = "none";
    }

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

    function logIn() {
        var user_log_in = document.getElementsByClassName("user_log_in")[0];
        if (user_log_in) {
            var alertbox = user_log_in.getElementsByClassName("alertbox")[0];
            alertbox.style.display = "none";
            log_in = user_log_in.getElementsByClassName("log_in")[0];
            if (log_in) {
                log_in.onclick = function (event) {
                    log_name = user_log_in.getElementsByClassName("log_name")[0];
                    if (log_name.value == "") {
                        alertbox.style.display = "block";
                        alertbox.innerHTML = "Name is missing!";
                        event.preventDefault();
                        return;
                    }
                    log_pass = user_log_in.getElementsByClassName("log_pass")[0];
                    if (log_pass.value == "") {
                        alertbox.style.display = "block";
                        alertbox.innerHTML = "Password is missing!";
                        event.preventDefault();
                        return;
                    }
                }
            }
        }
    }
}