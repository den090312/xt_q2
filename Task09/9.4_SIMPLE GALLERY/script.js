setTimeout(function () 
{
    var pageName = window.location.pathname.split("/").pop().split(".").shift();

    //alert(pageName);

    if (pageName == "index")
    {
        window.location.href = "1.html";    
    }
    else if (pageName == "5")
    {
        window.location.href = "index.html";        
    }
    else
    {
        var nextPageNumber = Number(pageName) + 1; 

        window.location.href = nextPageNumber + ".html"; 
    }

}, 2000);