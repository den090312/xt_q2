RunCountdown();

function RunCountdown()
{
    var seconds = 10;

    var interval = setInterval(function() 
    {
        document.getElementById("timer").innerHTML = seconds;
        seconds--;
    
        if (seconds < 0) 
        {
            clearInterval(interval);
            GoToLocation();
        }
      
    }, 1000);
}

function GoToLocation()
{
    var pageName = window.location.pathname.split("/").pop().split(".").shift();

    if (pageName == "index")
    {
        window.location.href = "1.html";    
    }
    else if (pageName == "5")
    {
        if (confirm("Continue?")) 
        {
            window.location.href = "index.html";  
        } 
        else 
        {
            window.open('', '_self', ''); 
            window.close();    
        }     
    }
    else
    {
        var nextPageNumber = Number(pageName) + 1; 

        window.location.href = nextPageNumber + ".html"; 
    }
}