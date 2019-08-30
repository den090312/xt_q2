RunCountdown();

function RunCountdown()
{
    var seconds = 9;

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
        window.location.href = "index.html";        
    }
    else
    {
        var nextPageNumber = Number(pageName) + 1; 

        window.location.href = nextPageNumber + ".html"; 
    }
}