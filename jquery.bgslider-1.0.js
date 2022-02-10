/**
 *
 */
(function($, window, document, undefined) {

	var pluginName = "bgslider", defaults = {
		prevNextButton : true,	//버튼 사용유무
		fadeStart : true,					//자동 시작여부
		fadeDelay : 2000,				//이미지 Fade ms
		delay : 3000						//이미지 변경 ms
	};

	function Plugin(element, options) {

		var sliderBox = $(element);
		var settings = $.extend({}, defaults, options);
		var sliderMax, activeNo = 0;
		var uniqNum;		//해당 Element의 고유 번호

		function init() {
			//HTML 생성
			console.log("init....");
			generateHtml();

			//자동실행 일경우 슬라이드 실행
			if(settings.fadeStart) {
				setTimeout(startSlider, settings.delay);
			}
		}

		//이미지 변경
		function changeSlider(c, n) {

			$(".imageDiv[id='" + uniqNum + "_" + c + "']").fadeOut(settings.fadeDelay);
			$(".imageDiv[id='" + uniqNum + "_" + n + "']").fadeIn(settings.fadeDelay);
		}

		//이전 버튼 클릭 이벤트
		function prevSlider(evt) {

			settings.fadeStart = false;

			var curSlide = activeNo;
			var nextSlide = activeNo - 1;

			if( nextSlide < 0 ) {
				nextSlide = activeNo = sliderMax;
			} else {
				activeNo--;
			}

			changeSlider(curSlide, nextSlide);
		}

		//다음 버튼 클릭 이벤트
		function nextSlider(evt) {

			settings.fadeStart = false;

			var curSlide = activeNo;
			var nextSlide = activeNo + 1;

			if( nextSlide > sliderMax ) {
				nextSlide = activeNo = 0;
			} else {
				activeNo++;
			}

			changeSlider(curSlide, nextSlide);
		}

		//슬라이드 시작
		function startSlider() {

			if( settings.fadeStart ) {

				var curSlide = activeNo;
				var nextSlide = activeNo + 1;

				if( nextSlide > sliderMax ) {
					nextSlide = activeNo = 0;
				} else {
					activeNo++;
				}

				changeSlider(curSlide, nextSlide);
				setTimeout(startSlider, settings.delay);
			}
		}

		//HTML 생성
		function generateHtml() {
			console.log("generateHtml....");
			var slider = sliderBox.children(".slider");
			console.log(sliderBox);
			slider.hide();//대상이 되는 이미지

			var slideImage = slider.children("li").children("img");

			activeNo = 0;
			sliderMax = slideImage.size() - 1;

			//고유한 키 생성
			uniqNum = Math.floor(Math.random() * 100) + 1;

			for( var i=0; i <= sliderMax; i++ ) {

				var imageSrc = $(slideImage.get(i)).attr("src");
				var html = "<div class='imageDiv' id='" + uniqNum + "_" + i + "' style=\"position:absolute;width:100%;height:" + sliderBox.height() +"px; " +
					"background:url('" + imageSrc + "') no-repeat center center; display:" + (i>0?"none":"block") + ";\"></div>";

				$(sliderBox).append(html);
			}

			if( settings.prevNextButton) {

				var buttons = "<div id='prevBtn' class='sliderBtn prevBtn" + uniqNum + "'></div><div id='nextBtn' class='sliderBtn nextBtn" + uniqNum + "'></div>";
				$(sliderBox).append(buttons);

				$(".prevBtn" + uniqNum).bind("click", prevSlider);
				$(".nextBtn" + uniqNum).bind("click", nextSlider);
			}

		}

		function selectSlider(key) {

			settings.fadeStart = false;

			if( activeNo == key ) return;

			var curSlide = activeNo;
			var nextSlide = activeNo = key;


			changeSlider(curSlide, nextSlide);
		}

		init();

		return {
			selectSlider : selectSlider
		};
	}

	$.fn[pluginName] = function(options) {

		if (typeof arguments[0] === 'string') {
			var methodName = arguments[0];
			var args = Array.prototype.slice.call(arguments, 1);
			var returnVal;
			
			this.each(function() {
				// Check that the element has a plugin instance, and that
				// the requested public method exists.
				if ($.data(this, 'plugin_' + pluginName) && typeof $.data(this, 'plugin_' + pluginName)[methodName] === 'function') {
					// Call the method of the Plugin instance, and Pass it
					// the supplied arguments.
					returnVal = $.data(this, 'plugin_' + pluginName)[methodName].apply(this, args);
				} else {
					throw new Error('Method ' +  methodName + ' does not exist on jQuery.' + pluginName);
				}
			});
      
			if (returnVal !== undefined){
			
				// If the method returned a value, return the value.
				return returnVal;
			} else {
				// Otherwise, returning 'this' preserves chainability.
				return this;
			}
		
		// If the first parameter is an object (options), or was omitted,
		// instantiate a new instance of the plugin.
		} else if (typeof options === "object" || !options) {
			return this.each(function() {
				// Only allow the plugin to be instantiated once.
				if (!$.data(this, 'plugin_' + pluginName)) {
					// Pass options to Plugin constructor, and store Plugin
					// instance in the elements jQuery data object.
					$.data(this, 'plugin_' + pluginName, new Plugin(this, options));
				}
			});
		};

	/*
		return this.each(function() {
			if( !$.data(this, "plugin_" + pluginName)) {
				$.data(this, "plugin_" + pluginName, new Plugin(this, options));
			} else {
				//
			}
		});
	*/
	};

}(jQuery, window, document));