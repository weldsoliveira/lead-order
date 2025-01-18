(function (document, navigator) {
	document.addEventListener('DOMContentLoaded', function () {
		document.querySelectorAll("img[src$='.svg']").forEach(function (imgTag) {
			var
				svg = imgTag,
				imgSRC = imgTag.getAttribute('src'),
				imgID = imgTag.getAttribute('id'),
				imgClasses = imgTag.classList,

				xhr = new XMLHttpRequest();
			if (!xhr.s) {
				xhr.s = [];
				xhr.open('GET', imgSRC);

				xhr.onload = function () {
					var x = document.createElement('x'), s = xhr.s;

					x.innerHTML = xhr.responseText;

					xhr.onload = function () {
						s.splice(0).map(function (array) {
							var svgElem = x.querySelector("svg");
							//TRECHO PARA REMOVER OS STYLES DO PATH PARA QUE SEJA POSSÍVEL A MODIFICAÇÃO VIA CSS
							svgElem.querySelectorAll("path").forEach(function (pathElement) {
								pathElement.removeAttribute("style");
							});
							//
							if (imgID)
								svgElem.id = imgID;
							if (imgClasses.value)
								svgElem.classList = imgClasses;
							if (svgElem)
								array[0].parentNode.replaceChild(svgElem.cloneNode(true), array[0]);
						});
					};

					xhr.onload();
				};

				xhr.send();
			}

			xhr.s.push([svg, imgTag, imgID]);

			if (xhr.responseText) xhr.onload();
		});
	});
})(
	document,
	navigator
);