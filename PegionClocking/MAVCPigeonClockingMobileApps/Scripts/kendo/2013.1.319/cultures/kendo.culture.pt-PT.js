/*
* Kendo UI Mobile v2013.1.518 (http://kendoui.com)
* Copyright 2013 Telerik AD. All rights reserved.
*
* Kendo UI Mobile commercial licenses may be obtained at
* https://www.kendoui.com/purchase/license-agreement/kendo-ui-mobile-commercial.aspx
* If you do not own a commercial license, this file shall be governed by the trial license terms.
*/
﻿(function( window, undefined ) {
    kendo.cultures["pt-PT"] = {
        name: "pt-PT",
        numberFormat: {
            pattern: ["-n"],
            decimals: 2,
            ",": ".",
            ".": ",",
            groupSize: [3],
            percent: {
                pattern: ["-n%","n%"],
                decimals: 2,
                ",": ".",
                ".": ",",
                groupSize: [3],
                symbol: "%"
            },
            currency: {
                pattern: ["-n $","n $"],
                decimals: 2,
                ",": ".",
                ".": ",",
                groupSize: [3],
                symbol: "€"
            }
        },
        calendars: {
            standard: {
                days: {
                    names: ["domingo","segunda-feira","terça-feira","quarta-feira","quinta-feira","sexta-feira","sábado"],
                    namesAbbr: ["dom","seg","ter","qua","qui","sex","sáb"],
                    namesShort: ["D","S","T","Q","Q","S","S"]
                },
                months: {
                    names: ["Janeiro","Fevereiro","Março","Abril","Maio","Junho","Julho","Agosto","Setembro","Outubro","Novembro","Dezembro",""],
                    namesAbbr: ["Jan","Fev","Mar","Abr","Mai","Jun","Jul","Ago","Set","Out","Nov","Dez",""]
                },
                AM: [""],
                PM: [""],
                patterns: {
                    d: "dd-MM-yyyy",
                    D: "dddd, d' de 'MMMM' de 'yyyy",
                    F: "dddd, d' de 'MMMM' de 'yyyy HH:mm:ss",
                    g: "dd-MM-yyyy HH:mm",
                    G: "dd-MM-yyyy HH:mm:ss",
                    m: "d/M",
                    M: "d/M",
                    s: "yyyy'-'MM'-'dd'T'HH':'mm':'ss",
                    t: "HH:mm",
                    T: "HH:mm:ss",
                    u: "yyyy'-'MM'-'dd HH':'mm':'ss'Z'",
                    y: "MMMM' de 'yyyy",
                    Y: "MMMM' de 'yyyy"
                },
                "/": "-",
                ":": ":",
                firstDay: 1
            }
        }
    }
})(this);
