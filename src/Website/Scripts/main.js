/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/modernizr/modernizr.d.ts" />
/// <reference path="typings/jqueryui/jqueryui.d.ts" />
/// <reference path="typings/custom/custom.d.ts" />
/// <reference path="typings/jquery.cookie/jquery.cookie.d.ts" />
/// <reference path="typings/jquery.ui.datetimepicker/jquery.ui.datetimepicker.d.ts" />
var Prekenweb;
(function (Prekenweb_1) {
    'use strict';
    var Prekenweb = /** @class */ (function () {
        function Prekenweb() {
            var _this = this;
            this.taal = "";
            this.inMijn = false;
            this.authenticated = false;
            this.zoekOpdrachtOpslaanNaarFeed = function (sender) {
                var pw = _this;
                sender.innerHTML = "Zoek opdracht opslaan...";
                $.ajax({
                    url: sender.href,
                    success: function (data) {
                        sender.innerHTML = "Zoek opdracht opgeslagen, feed laden...";
                        window.location.href = pw.rootUrl + pw.taal + '/Home/RssFeed/' + data;
                    },
                    error: function () {
                        alert('Kon zoekopdracht helaas niet opslaan...');
                    }
                });
            };
            this.preekBezochtChecksTonen = function (preekIds) {
                if (!$.cookie('Token'))
                    return;
                var apiRootUrl = prekenweb.apiRootUrl.replace(/\/$/, '');
                $.ajax({
                    type: 'GET',
                    url: apiRootUrl + '/api/Gebruiker/GeopendePreken',
                    data: { preekIds: preekIds },
                    headers: { "Authorization": "Bearer " + $.cookie('Token') },
                    //dataType: 'json',
                    contentType: 'application/json',
                    success: function (preekCookies) {
                        for (var i = 0; i < preekCookies.length; i++) {
                            var preekCookie = preekCookies[i];
                            if (preekCookie.DateTime != null) { // preek is bezocht
                                // vinkje weergeven achter preek
                                var bezochtCheckElement = $(".preek-bezocht-check-" + preekCookie.PreekId);
                                bezochtCheckElement.show();
                                bezochtCheckElement.attr("title", bezochtCheckElement.attr("title") + " " + new Date(preekCookie.DateTime).toLocaleDateString());
                                $(".datum-bezocht", bezochtCheckElement).text(new Date(preekCookie.DateTime).toLocaleDateString());
                            }
                            // in zoekresultaten, bladwijzer icoon actief maken
                            var bladwijzerIcoonElement = $(".preek-bladwijzer-" + preekCookie.PreekId);
                            $("span", bladwijzerIcoonElement).removeClass("fa-bookmark");
                            $("span", bladwijzerIcoonElement).removeClass("fa-bookmark-o");
                            if (preekCookie.BladwijzerGelegdOp == null) {
                                $("span", bladwijzerIcoonElement).addClass("fa-bookmark-o");
                                bladwijzerIcoonElement.data("active", "False");
                            }
                            else {
                                $("span", bladwijzerIcoonElement).addClass("fa-bookmark");
                                bladwijzerIcoonElement.data("active", "True");
                            }
                        }
                    },
                    error: function (XHR, textStatus, errorThrown) {
                        $.removeCookie("Token");
                    }
                });
            };
        }
        Prekenweb.prototype.dataRelatedTooltips = function () {
            var pw = this;
            $('a[data-linktype]').each(function (i, sender) {
                if ($(sender).data("relid") == undefined || $(sender).data("relid") === "")
                    return;
                var url = pw.rootUrl + pw.taal + '/Tooltip/' + $(sender).data("linktype") + '/' + $(sender).data("relid");
                if ($(sender).data("preek-id") != undefined) {
                    url = url + "?preekId=" + $(sender).data("preek-id");
                }
                $(sender).qtip({
                    content: {
                        text: 'Laden...',
                        ajax: {
                            url: url
                        },
                        title: {
                            text: '' + $(sender).text(),
                            button: true
                        }
                    },
                    position: {
                        my: 'bottom center',
                        at: 'top center',
                        viewport: $(window),
                        adjust: {
                            target: $(document),
                            resize: true // Can be ommited (e.g. default behaviour)
                        }
                    },
                    show: {
                        event: 'click',
                        solo: true
                    },
                    hide: {
                        event: 'unfocus'
                    },
                    style: {
                        classes: 'qtip-tipsy qtip-shadow'
                    }
                });
            }).bind('click', function (event) { event.preventDefault(); return false; });
        };
        //
        // werden gebruikt op verschillende locaties voor inline popup weergave maar is ondertussen nergens meer in gebruik
        //
        //public prekenwebPopup(url, width, height) {
        //    if (typeof (width) === 'undefined') width = 750;
        //    if (typeof (height) === 'undefined') height = 450;
        //    var tag = $("<div class='prekenwebPopup'><iframe src='" + url + "' /></div>");
        //    tag.dialog({
        //        modal: true,
        //        width: width,
        //        height: height,
        //        position: [100, 100],
        //        open: function () {
        //            $('.ui-widget-overlay').bind('click', function () {
        //                $('.prekenwebPopup').dialog('close');
        //            });
        //            $(".ui-dialog-titlebar").hide();
        //        }
        //    });
        //}
        // jquery.ajax.unobtrusive werkt nog niet met jQuery 2.0, daarom zelf even een functionaliteit gemaakt :)
        Prekenweb.prototype.unobtrusiveAjaxReplacement = function () {
            // 05-05-2015 uitgezet, zorge voor een dubbele http request op homepage
            //$(document).on('click', "a[data-ajax='true']", function (e) {
            //    var url = $(this).attr("href");
            //    var target = $(this).data("ajaxUpdate");
            //    $.ajax({
            //        url: url,
            //        success: function (data) {
            //            $(target).html(data);
            //            this.standaardTooltip();
            //            this.dataRelatedTooltips();
            //        }
            //    });
            //    e.preventDefault();
            //});
        };
        //
        // TODO: was bedoelt voor als je de site toevoegd in een iPad, zodat hij niet na iedere touch naar de browser springt maar zit in de web met javascript links
        // pas weer aanzetten als javascript links wordt geexclude
        //
        //// prevents links from apps from oppening in mobile safari
        //// this javascript must be the first script in your <head>
        //(function (document, navigator, standalone) {
        //    if ((standalone in navigator) && navigator[standalone]) {
        //        var curnode, location = document.location, stop = /^(a|html)$/i;
        //        document.addEventListener('click', function (e) {
        //            curnode = e.target;
        //            while (!(stop).test(curnode.nodeName)) {
        //                curnode = curnode.parentNode;
        //            }
        //            // Condidions to do this only on links to your own app
        //            // if you want all links, use if('href' in curnode) instead.
        //            if ('href' in curnode && (curnode.href.indexOf('http') || curnode.href.indexOf(location.host))) {
        //                e.preventDefault();
        //                $("#loading").show();
        //                $("#pageContent").hide();
        //                location.href = curnode.href;
        //            }
        //        }, false);
        //    }
        //})(document, window.navigator, 'standalone');
        Prekenweb.prototype.datePickers = function () {
            var pw = this;
            $('[type="date"]').datepicker({
                dateFormat: pw.taal == "nl" ? "dd-mm-yy" : "mm/dd/yy"
            });
            $('[type="datetime"]').datetimepicker({
                dateFormat: pw.taal == "nl" ? "dd-mm-yy" : "mm/dd/yy",
                timeFormat: pw.taal == "nl" ? "HH:mm:ss" : "h:m:ss TT"
            });
        };
        Prekenweb.prototype.preekTypeCombo = function () {
            var pw = this;
            $("select[name='PreekTypeId']").each(function (index) {
                pw.changePreekType($(this).val());
            });
        };
        Prekenweb.prototype.standaardTooltip = function () {
            $(".tooltip[title],.KleineIconKnop[title]").qtip({
                position: {
                    my: 'bottom center',
                    at: 'top center',
                    viewport: $("#container")
                },
                style: {
                    classes: 'qtip-tipsy qtip-shadow'
                }
            });
        };
        Prekenweb.prototype.html5Placeholder = function () {
            if (!Modernizr.input.placeholder) {
                $('[placeholder]').focus(function () {
                    var input = $(this);
                    if (input.val() == input.attr('placeholder')) {
                        input.val('');
                        input.removeClass('placeholder');
                    }
                }).blur(function () {
                    var input = $(this);
                    if (input.val() == '' || input.val() == input.attr('placeholder')) {
                        input.addClass('placeholder');
                        input.val(input.attr('placeholder'));
                    }
                }).blur();
                $('[placeholder]').parents('form').submit(function () {
                    $(this).find('[placeholder]').each(function () {
                        var input = $(this);
                        if (input.val() == input.attr('placeholder')) {
                            input.val('');
                        }
                    });
                });
            }
        };
        Prekenweb.prototype.initializeZoeker = function () {
            var pw = this;
            $('html').click(function () {
                pw.VerbergZoeker();
            });
            $('#zoekresultatenContainer').click(function (event) {
                event.stopPropagation();
            });
            $("#inputZoek").bind('keyup', function (e) {
                clearTimeout(pw.zoekerTimeout);
                pw.zoekerTimeout = null;
                if ($(this).val() == "" || e.keyCode == 27) {
                    pw.VerbergZoeker();
                    return;
                }
                if (e.keyCode == 13) {
                    pw.VerwerkZoekActie();
                }
                else {
                    pw.ToonZoeker();
                    pw.zoekerTimeout = setTimeout(function () { pw.VerwerkZoekActie(); }, 2500);
                }
            });
        };
        Prekenweb.prototype.VerwerkZoekActie = function () {
            var pw = this;
            this.ToonLaderInZoeker();
            $.ajax({
                type: 'GET',
                data: { Zoekterm: $("#inputZoek").val() },
                url: pw.rootUrl + pw.taal + "/Zoeken/PartialInlineZoek",
                success: function (data) {
                    $("#zoekresultatenContainer").html(data);
                    $("tr:odd").addClass("oneven");
                    $("tr").not(':first').hover(function () {
                        $(this).addClass("hover");
                    }, function () {
                        $(this).removeClass("hover");
                    });
                    $("tr").not(':first').click(function () {
                        window.location.href = $(".ResultaatColLink a", this).attr("href");
                    });
                    pw.dataRelatedTooltips();
                    pw.standaardTooltip();
                },
                error: function () {
                    $("#zoekresultatenContainer").html("<center><br/><br/>Oeps. er ging iets fout... :(</center>");
                }
            });
        };
        Prekenweb.prototype.ToonZoeker = function () {
            $("#LoadingIndicator").remove();
            $("#zoekresultatenContainer").html("");
            $("#zoekresultatenContainer").append("<center id='enterTekst'><br/><br/>Druk op enter om te zoeken</center>");
            $("#driehoek").show();
            $("#zoekresultatenContainer").show();
            $("#container").css("opacity", "0.4");
        };
        Prekenweb.prototype.ToonLaderInZoeker = function () {
            var pw = this;
            $("#enterTekst").remove();
            $("#zoekresultatenContainer").append("<img src='" + pw.rootUrl + "/Content/Images/loading.gif' id='LoadingIndicator'/>");
        };
        Prekenweb.prototype.VerbergZoeker = function () {
            $("#driehoek").hide();
            $("#zoekresultatenContainer").hide();
            $("#container").css("opacity", "1");
        };
        Prekenweb.prototype.changePreekType = function (preekTypeId) {
            $(".LezingCategorieEditor").hide();
            $(".alleenBijLeespreek").hide();
            $(".alleenBijMeditatie").hide();
            switch (preekTypeId) {
                case "1": //preek
                    break;
                case "2": //lezing
                    $(".LezingCategorieEditor").show();
                    break;
                case "3": // leespreek
                    $(".alleenBijLeespreek").show();
                    break;
                case "4": // meditatie
                    $(".alleenBijMeditatie").show();
                    break;
            }
            $(".tabs").tabs();
            $("#leespreekTab").hide();
            $("#meditatieTab").hide();
        };
        Prekenweb.prototype.automatischeTekstenChanged = function () {
            var value = $("input[name=AutomatischeTeksten]:checked").val();
            if (value == "True") {
                $("#HandmatigeBijbeltekst").hide();
                $("#AutomatischeBijbeltekst").show();
            }
            else if (value == "False") {
                $("#HandmatigeBijbeltekst").show();
                $("#AutomatischeBijbeltekst").hide();
            }
        };
        Prekenweb.prototype.updateAutomatischeBijbeltekst = function () {
            var pw = this;
            var versVanId = $("#VersVanId").val();
            var versTotId = $("#VersTotId").val();
            var vanVersOmschrijving = $("#VersVanId_display").text().split(': ')[1];
            var totVersOmschrijving = $("#VersTotId_display").text().split(': ')[1];
            vanVersOmschrijving = (vanVersOmschrijving == undefined ? "" : vanVersOmschrijving);
            totVersOmschrijving = (totVersOmschrijving == undefined ? "" : " - " + totVersOmschrijving);
            $("#VersOmschrijving").val(vanVersOmschrijving + totVersOmschrijving);
            $("#AutomatischeBijbeltekst .value").html("Teskten ophalen...");
            $.ajax({
                url: pw.rootUrl + pw.taal + '/Preek/Bijbelgedeelte',
                data: { versVanId: versVanId, versTotId: versTotId },
                success: function (data) {
                    $("#AutomatischeBijbeltekst .value").html(data);
                },
                error: function (data) {
                    $("#AutomatischeBijbeltekst .value").html("fout!?");
                }
            });
        };
        Prekenweb.prototype.prekenWebMenu = function (show) {
            if (show == undefined)
                show = !$("#PrekenwebTabMijn").hasClass("active");
            if (show) {
                $("#zoeker").css("visibility", "hidden");
                $(".DefaultMenu").hide("fade", {}, 300, function () {
                    $(".MijnMenu").show("fade", {}, 300);
                });
                $("#PrekenwebTabMijn").addClass("active");
                $("#PrekenwebTabMijn a span").removeClass("fa-chevron-down");
                $("#PrekenwebTabMijn a span").addClass("fa-chevron-up");
            }
            else {
                $(".MijnMenu").hide("fade", {}, 300, function () {
                    $("#zoeker").show("fade", {}, 300);
                    $(".DefaultMenu").show("fade", {}, 300);
                });
                ;
                $("#PrekenwebTabMijn").removeClass("active");
                $("#PrekenwebTabMijn a span").removeClass("fa-chevron-up");
                $("#PrekenwebTabMijn a span").addClass("fa-chevron-down");
            }
        };
        Prekenweb.prototype.toonMijnReclame = function (reden) {
            $(".prekenwebPopup").remove();
            var url = this.rootUrl + this.taal + '/PrekenWeb/UMoetInloggen?reden=' + reden;
            //window.open(, "Print", "height=600,width=600,scrollbars=yes,status=no,titlebar=no,toolbar=no");
            var width = window.innerWidth < 750 ? window.innerWidth - 50 : 750;
            var height = window.innerWidth < 750 ? undefined : 350;
            var tag = $("<div class='prekenwebPopup'>Laden...</div>");
            tag.dialog({
                modal: true,
                width: width,
                height: height,
                //position: [100, 100],
                open: function () {
                    $("#InloggenMarkering").show();
                    $('.ui-widget-overlay').bind('click', function () {
                        $('.prekenwebPopup').dialog('destroy').remove();
                        $("#InloggenMarkering").hide();
                    });
                    $(".ui-dialog-titlebar").hide();
                }
            });
            $.ajax({
                url: url,
                success: function (data) {
                    $(".prekenwebPopup").html(data);
                }
            });
        };
        Prekenweb.prototype.toonVerskiezer = function (url) {
            //window.open(, "Print", "height=600,width=600,scrollbars=yes,status=no,titlebar=no,toolbar=no");
            var width = window.innerWidth < 750 ? window.innerWidth - 50 : 750;
            var height = window.innerHeight < 750 ? window.innerHeight - 10 : 750;
            var tag = $("<div class='prekenwebPopup'>Laden...</div>");
            tag.dialog({
                modal: true,
                width: width,
                height: height,
                //position: [100, 100],
                open: function () {
                    $('.ui-widget-overlay').bind('click', function () {
                        $('.prekenwebPopup').dialog('destroy').remove();
                    });
                    $(".ui-dialog-titlebar").hide();
                }
            });
            $.ajax({
                url: url,
                success: function (data) {
                    $(".prekenwebPopup").html(data);
                },
                error: function () {
                    $('.prekenwebPopup').dialog('destroy').remove();
                }
            });
        };
        Prekenweb.prototype.bladwijzerClick = function (sender, event) {
            var pw = this;
            event.stopPropagation();
            if (!pw.authenticated) {
                pw.toonMijnReclame("Bladwijzer");
                return false;
            }
            if ($(sender).data("active") === "True") {
                pw.bladwijzerVerwijderenVanuitZoekresultaten(sender);
            }
            else {
                pw.bladwijzerLeggenVanuitZoekresultaten(sender);
            }
            return false;
        };
        Prekenweb.prototype.bladWijzerMouseOver = function () {
            $(this).removeClass('fa-bookmark-o');
            $(this).addClass('fa-bookmark');
        };
        Prekenweb.prototype.bladWijzerMouseOut = function () {
            $(this).removeClass('fa-bookmark');
            if ($(this).parent().data("active") === "True") {
                $(this).addClass('fa-bookmark');
            }
            else {
                $(this).addClass('fa-bookmark-o');
            }
        };
        Prekenweb.prototype.bladwijzerLeggenVanuitZoekresultaten = function (sender) {
            var pw = this;
            $.ajax({
                url: pw.rootUrl + pw.taal + '/Preek/LegBladwijzer',
                data: { preekId: $(sender).data("preekid") },
                success: function () {
                    $("span", sender).removeClass("fa-bookmark-o");
                    $("span", sender).addClass("fa-bookmark");
                    $(sender).data("active", "True");
                    $("span", sender).attr("title", "Bladwijzer zojuist geplaatst");
                    $("span", sender).qtip();
                    $("span", sender).effect("shake", { distance: 5 });
                },
                error: function () {
                    alert('Kon bladwijzer helaas niet leggen...');
                }
            });
        };
        Prekenweb.prototype.bladwijzerVerwijderenVanuitZoekresultaten = function (sender) {
            var pw = this;
            $.ajax({
                url: pw.rootUrl + pw.taal + '/Preek/VerwijderBladwijzer',
                data: { preekId: $(sender).data("preekid") },
                success: function () {
                    $("span", sender).removeClass("fa-bookmark");
                    $("span", sender).addClass("fa-bookmark-o");
                    $(sender).data("active", "False");
                    $("span", sender).attr("title", "Bladwijzer zojuist verwijderd");
                    $("span", sender).qtip();
                    $("span", sender).effect("shake", { distance: 5 });
                },
                error: function () {
                    alert('Kon bladwijzer helaas niet leggen...');
                }
            });
        };
        Prekenweb.prototype.ZoekOpdrachtOpslaan = function (sender) {
            var pw = this;
            $("div", sender).addClass("fa-spin");
            $.ajax({
                url: sender.href,
                success: function () {
                    $("div", sender).removeClass("fa-spin");
                    $("div", sender).css("color", "#bfb544");
                    $("div", sender).effect("pulsate", {});
                },
                error: function () {
                    alert('Kon zoekopdracht helaas niet opslaan...');
                }
            });
        };
        Prekenweb.prototype.RssFeed = function (sender) {
            var pw = this;
            $("#RssFeedModal").modal();
        };
        return Prekenweb;
    }());
    Prekenweb_1.Prekenweb = Prekenweb;
})(Prekenweb || (Prekenweb = {}));
var prekenweb = new Prekenweb.Prekenweb();
$(document).ready(function () {
    prekenweb.html5Placeholder();
    prekenweb.initializeZoeker();
    prekenweb.datePickers();
    prekenweb.preekTypeCombo();
    prekenweb.standaardTooltip();
    prekenweb.unobtrusiveAjaxReplacement();
    prekenweb.dataRelatedTooltips();
    prekenweb.prekenWebMenu(prekenweb.inMijn);
    $(".tabs").tabs();
});
//# sourceMappingURL=main.js.map