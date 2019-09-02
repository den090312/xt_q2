function MoveFullRight(obj)
{
	CleanErrorMessage(obj);

	var availableSelectbox = GetDivElement(obj, "available").getElementsByTagName("select")[0]; 
	var selectedSelectbox = GetDivElement(obj, "selected").getElementsByTagName("select")[0];
	
	var length = availableSelectbox.options.length; 

	if (length > 0)
	{
		AddAllToSelectbox(availableSelectbox, selectedSelectbox);
		CleanSelectbox(availableSelectbox);
	}
} 

function MoveFullLeft(obj)
{
	CleanErrorMessage(obj);

	var availableSelectbox = GetDivElement(obj, "available").getElementsByTagName("select")[0]; 
	var selectedSelectbox = GetDivElement(obj, "selected").getElementsByTagName("select")[0];
	
	var length = selectedSelectbox.options.length; 

	if (length > 0)
	{
		AddAllToSelectbox(selectedSelectbox, availableSelectbox);	
		CleanSelectbox(selectedSelectbox);
	}
} 

function MoveSelectedLeft(obj)
{
	CleanErrorMessage(obj);

	var selectedSelectbox = GetDivElement(obj, "selected").getElementsByTagName("select")[0]; 

	if (SelectionExists(selectedSelectbox))
	{
		var options = GetSelectedOptions(selectedSelectbox);
		var availableSelectbox = GetDivElement(obj, "available").getElementsByTagName("select")[0];

		AddOptions(options, availableSelectbox);
		RemoveOptions(options, selectedSelectbox);	
		CleanErrorMessage(obj);
	}
	else
	{
		PrintErrorMessage(obj, "No options selected!");
	}
} 

function MoveSelectedRight(obj)
{
	CleanErrorMessage(obj);

	var availableSelectbox = GetDivElement(obj, "available").getElementsByTagName("select")[0]; 

	if (SelectionExists(availableSelectbox))
	{
		var options = GetSelectedOptions(availableSelectbox);
		var selectedSelectbox = GetDivElement(obj, "selected").getElementsByTagName("select")[0];

		AddOptions(options, selectedSelectbox);
		RemoveOptions(options, availableSelectbox);
		CleanErrorMessage(obj);
	}
	else
	{
		PrintErrorMessage(obj, "No options selected!");
	}
} 

function AddAllToSelectbox(selectboxFrom, selectboxTo)
{
	AddOptions(GetAllOptions(selectboxFrom), selectboxTo);
}

function GetAllOptions(selectbox)
{
    var selectedOptions = [];

    for (var i = 0; i < selectbox.length; i++) 
	{
		var option = selectbox.options[i];
		
		selectedOptions.push(option.text);
    }

	return selectedOptions; 
}

function GetSelectedOptions(selectbox)
{
    var selectedOptions = [];

    for (var i = 0; i < selectbox.length; i++) 
	{
        var option = selectbox.options[i];

		if (option.selected) 
		{
			selectedOptions.push(option.text);
		}
    }

	return selectedOptions; 
} 

function AddOptions(options, selectbox)
{
	for (let option of options)
	{
		var newOption = document.createElement("option");
		newOption.text = option;
		
		selectbox.appendChild(newOption);
	}
}

function RemoveOptions(options, selectbox)
{
	for (var i = 0; i < selectbox.length; i++) 
	{
		var text = selectbox.options[i].text; 

		if (options.includes(text))
		{
			selectbox.remove(i);
			i--;
		}
	}
}

function CleanSelectbox(selectbox)
{
	do
	{
		selectbox.remove(0);
	}
	while (selectbox.options.length > 0);
} 

function SelectionExists(selectbox)
{
	return selectbox.selectedIndex != -1;
}

function GetDivElement(obj, className)
{
	var butterfly = obj.parentNode.parentNode.parentNode;

	for (let child of butterfly.children)
	{
		if (child.className == className)
		{
			return child;  
		}
	}	
}

function PrintErrorMessage(obj, errorText)
{
	GetDivElement(obj, "error_message").textContent = errorText;
}

function CleanErrorMessage(obj)
{
	GetDivElement(obj, "error_message").textContent  = "";	
}