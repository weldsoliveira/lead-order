﻿/* Brazilian initialisation for the jQuery UI date picker plugin. */
/* Written by Leonildo Costa Silva (leocsilva@gmail.com). */
(function (factory) {
	if (typeof define === "function" && define.amd) {

		// AMD. Register as an anonymous module.
		define(["../widgets/datepicker"], factory);
	} else {

		// Browser globals
		factory(jQuery.datepicker);
	}
}(function (datepicker) {

	datepicker.regional["pt-BR"] = {
		closeText: "Fechar",
		prevText: "Anterior",
		nextText: "Próximo",
		monthNames: ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho",
			"Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"],
		monthNamesShort: ["Jan", "Fev", "Mar", "Abr", "Mai", "Jun",
			"Jul", "Ago", "Set", "Out", "Nov", "Dez"],
		dayNames: [
			"Domingo",
			"Segunda-feira",
			"Terça-feira",
			"Quarta-feira",
			"Quinta-feira",
			"Sexta-feira",
			"Sábado"
		],
		dayNamesShort: ["D", "S", "T", "Q", "Q", "S", "S"],
		dayNamesMin: ["D", "S", "T", "Q", "Q", "S", "S"],
		weekHeader: "Sm",
		dateFormat: "dd/mm/yy",
		firstDay: 0,
		isRTL: false,
		showMonthAfterYear: false,
		yearSuffix: ""
	};
	datepicker.setDefaults(datepicker.regional["pt-BR"]);

	return datepicker.regional["pt-BR"];

}));