//var optionQuantity = 5;

function MoveFullRight(obj)
{
	var availableSelect = GetDivElement(obj, "available").getElementsByTagName("select")[0]; 
	var selectedSelect = GetDivElement(obj, "selected").getElementsByTagName("select")[0];
	
	var length = availableSelect.options.length; 

	if (length > 0)
	{
		AddAllToSelectbox(availableSelect, selectedSelect);
		CleanSelectbox(availableSelect);
	}
} 

function MoveFullLeft(obj)
{
	var availableSelect = GetDivElement(obj, "available").getElementsByTagName("select")[0]; 
	var selectedSelect = GetDivElement(obj, "selected").getElementsByTagName("select")[0];
	
	var length = selectedSelect.options.length; 

	if (length > 0)
	{
		AddAllToSelectbox(selectedSelect, availableSelect);	
		CleanSelectbox(selectedSelect);
	}
} 

function MoveSelectedRight(obj)
{
	var availableSelect = GetDivElement(obj, "available").getElementsByTagName("select")[0]; 

	if (SelectExists(availableSelect))
	{
		var options = GetSelectedOptions(availableSelect);
		var selectedSelect = GetDivElement(obj, "selected").getElementsByTagName("select")[0];

		AddOptions(options, selectedSelect);
		RemoveOptions(options, availableSelect);
	}
	else
	{
		alert("No options selected!");
	}
} 

function MoveSelectedLeft(obj)
{
	var selectedSelect = GetDivElement(obj, "selected").getElementsByTagName("select")[0]; 

	if (SelectExists(selectedSelect))
	{
		var options = GetSelectedOptions(selectedSelect);
		var availableSelect = GetDivElement(obj, "available").getElementsByTagName("select")[0];

		AddOptions(options, availableSelect);
		RemoveOptions(options, selectedSelect);	
	}
	else
	{
		alert("No options selected!");
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

function RemoveOptions(options, selectObject)
{
	for (var i = 0; i < selectObject.length; i++) 
	{
		var text = selectObject.options[i].text; 

		if (options.includes(text))
		{
			selectObject.remove(i);
			i--;
		}
	}
}

function AddNewOption(selectbox, index)
{
	var option = document.createElement("option");
	option.text = "Option " + index;

	selectbox.appendChild(option);		
}

function CleanSelectbox(selectbox)
{
	do
	{
		selectbox.remove(0);
	}
	while (selectbox.options.length > 0);
} 

function SelectExists(selectbox)
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