function getReceipt() {
	// This initializes our string so it can get passed from  
	// function to function, growing line by line into a full receipt
	var text1 = "<h3>You Ordered:</h3>";
	var runningTotal = 0;
	var sizeTotal = 0;
	var sizeArray = document.getElementsByClassName("size");
	for (var i = 0; i < sizeArray.length; i++) {
		if (sizeArray[i].checked) {
			var selectedSize = sizeArray[i].value;
			text1 = text1+selectedSize+"<br>";
		}
	}
	if (selectedSize === "Personal Pizza") {
		sizeTotal = 6;
	} else if (selectedSize === "Medium Pizza") {
		sizeTotal = 10;
	} else if (selectedSize === "Large Pizza") {
		sizeTotal = 14;
	} else if (selectedSize === "Extra Large Pizza") {
		sizeTotal = 16;
	}
	runningTotal = sizeTotal;
	getCrust(runningTotal,text1); // All three of these variables will be passed on to each function
};
function getCrust(runningTotal,text1) {
	var crustTotal = 0;
	var selectedCrust = [];
	var crustArray = document.getElementsByClassName("crust");
	for (var i = 0; i < crustArray.length; i++) {
		if (crustArray[i].checked) {
			var selectedCrust = crustArray[i].value;
			text1 = text1+selectedCrust+"<br>";
		}
	}
	if (selectedCrust === "Cheese Stuffed Crust") {
		crustTotal = 3;
	} else crustTotal = 0; {
	}
	runningTotal = (runningTotal + crustTotal);
	getCheese(runningTotal,text1);
};	
function getCheese(runningTotal,text1) {
	var cheeseTotal = 0;
	var selectedCheese = [];
	var cheeseArray = document.getElementsByClassName("cheese");
	for (var i = 0; i < cheeseArray.length; i++) {
		if (cheeseArray[i].checked) {
			var selectedCheese = cheeseArray[i].value;
			text1 = text1+selectedCheese+"<br>";
		}
	}
	if (selectedCheese === "Extra Cheese") {
		cheeseTotal = 3;
	} else cheeseTotal = 0; {
	}
	runningTotal = (runningTotal + cheeseTotal);
	getMeat(runningTotal,text1);
};	
function getMeat(runningTotal,text1) {
	var meatTotal = 0;
	var selectedMeat = [];
	var meatArray = document.getElementsByClassName("meats");
	for (var j = 0; j < meatArray.length; j++) {
		if (meatArray[j].checked) {
			selectedMeat.push(meatArray[j].value);
			console.log("selected meat item: ("+meatArray[j].value+")");
			text1 = text1+meatArray[j].value+"<br>";
		}
	}
	var meatCount = selectedMeat.length;
	if (meatCount > 1) {
		meatTotal = (meatCount - 1);
	} else {
		meatTotal = 0;
	}
	runningTotal = (runningTotal + meatTotal);
	getVegetable(runningTotal,text1);
};		
function getVegetable(runningTotal,text1) {
	var vegetableTotal = 0;
	var selectedVegetable = [];
	var vegetableArray = document.getElementsByClassName("vegetables");
	for (var j = 0; j < vegetableArray.length; j++) {
		if (vegetableArray[j].checked) {
			selectedVegetable.push(vegetableArray[j].value);
			console.log("selected vegetable item: ("+vegetableArray[j].value+")");
			text1 = text1+vegetableArray[j].value+"<br>";
		}
	}
	var vegetableCount = selectedVegetable.length;
	if (vegetableCount > 1) {
		vegetableTotal = (vegetableCount - 1);
	} else {
		vegetableTotal = 0;
	};
	runningTotal = (runningTotal + vegetableTotal);
	console.log("Purchase Total: "+"$"+runningTotal+".00");
	document.getElementById("showText").innerHTML=text1;
	document.getElementById("totalPrice").innerHTML = "</h3>Total: <strong>$"+runningTotal+".00"+"</strong></h3>";
};
	
	
	