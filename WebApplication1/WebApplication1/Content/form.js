var provinceArray = new Array(4);
provinceArray[0] = ["河南", "郑州", "开封", "洛阳", "信阳", "南阳", "固始"];
provinceArray[1] = ["浙江", "杭州", "绍兴", "宁波", "诸暨"];
provinceArray[2] = ["江苏", "徐州", "泰州", "常州", "无锡"];
provinceArray[3] = ["台湾", "台北", "桃园", "台南", "高雄"];

function cityAdd(value) {

	/*
		1.遍历二维数组
		2.得到的第一个值与传过来的value比较
		3.如果相同，获取后面的值
		4.得到city的select标签
		5.select添加option
			-创建option标签
			-创建文本
			-将文本添加进option
			-select.appendChild(option)
	*/

	/*
		由于每次向option，都是在末尾添加，所以每次在末尾追加后，上一次追加的还在，
		应当每次判断是否有option，如果有，用父节点删除
	*/
	var city = document.getElementById("city");
	var cityOption = city.getElementsByTagName("option");
	for (var i = 0; i < cityOption.length; i++) {
		city.removeChild(cityOption[i]);
		i--;
	}

	for (var i = 0; i < provinceArray.length; i++) {
		var cityArray = provinceArray[i];
		if (cityArray[0] == value) {
			for (var j = 1; j < cityArray.length; j++) {
				var option = document.createElement("option");
				var text = document.createTextNode(cityArray[j]);
				option.appendChild(text);
				city.appendChild(option);
			}

		}
	}

}
