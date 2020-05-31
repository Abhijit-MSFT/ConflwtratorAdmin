$(document).ready(function () {
    var navListItems = $("div.setup-panel div a"),
        navListspan = $("div.setup-panel div span.nt"),
        allWells = $(".setup-content"),
        allNextBtn = $(".nextBtn");
    $("#step-2").hide();
    $("#step-3").hide();
    $(navListItems[0]).addClass("btn-primary");

    navListItems.click(function (e) {
        e.preventDefault();
        var $target = $($(this).attr("href")),
            $item = $(this);

        if (!$item.hasClass("disabled")) {
            navListItems.removeClass("btn-primary").addClass("btn-default");
            $item.addClass("btn-primary");
            allWells.hide();
            $target.show();
            $target.find("input:eq(0)").focus();
        }
    });
    navListspan.click(function (e) {
        e.preventDefault();
        var $target = $(
            $(this)
                .parent()
                .find("a")
                .attr("href")
        ),
            $item = $(this)
                .parent()
                .find("a");

        if (!$item.hasClass("disabled")) {
            navListItems.removeClass("btn-primary").addClass("btn-default");
            $item.addClass("btn-primary");
            allWells.hide();
            $target.show();
            $target.find("input:eq(0)").focus();
        }
    });

    allNextBtn.click(function () {
        var curStep = $(this).closest(".setup-content"),
            curStepBtn = curStep.attr("id"),
            nextStepWizard = $('div.setup-panel div a[href="#' + curStepBtn + '"]')
                .parent()
                .next()
                .children("a"),
            curInputs = curStep.find("input[type='text'],input[type='url']"),
            isValid = true;

        $(".form-group").removeClass("has-error");
        for (var i = 0; i < curInputs.length; i++) {
            if (!curInputs[i].validity.valid) {
                isValid = false;
                $(curInputs[i])
                    .closest(".form-group")
                    .addClass("has-error");
            }
        }

        if (isValid) nextStepWizard.removeAttr("disabled").trigger("click");
    });

    $("div.setup-panel div a.btn-primary").trigger("click");

    $(".custom-file-input").on("change", function () {
        var fileName = $(this).val();
        $(this)
            .siblings(".custom-file-label")
            .addClass("selected")
            .html(fileName);
    });
    $("div[id^=b]").hide();
    $("input[id^=i]").change(function () {
        var index = $(this)
            .attr("id")
            .replace("i", "");
        if ($(this).is(":checked")) $("#b" + index).show();
        else $("#b" + index).hide();
    });

    function uncheckAll() {
        $("input[type='checkbox']:checked").prop("checked", false);
        $("div[id^=b]").hide();
    }
    $(":button").on("click", uncheckAll);

    $("div > span").click(function () {
        $(".nt").removeClass("active");
        $(this).addClass("active");
    });

    $(window).on('load', function () {
        $("#coverScreen").hide();
    });

    $(document).on('submit', 'form', function () {
        displayBusyIndicator();
    });
});

function displayBusyIndicator() {
    $('.loading').show();
}

function checkBTNApplicationDetails() {
    var disName = document.getElementById("mainForm").elements["displayName"].value;
    var appDes = document.getElementById("mainForm").elements["appDesc"].value;
    if (disName && appDes) {
        document.getElementById("btnApplicationDetails").style.backgroundColor = "Green";
        document.getElementById("btnApplicationDetails").style.color = "White";
    }
    else {
        document.getElementById("btnApplicationDetails").style.backgroundColor = "Red";
        document.getElementById("btnApplicationDetails").style.color = "White";
    }
    if (disName == "") {
        $("#displayName").addClass('dangerinput');
    }
    else {
        $("#displayName").removeClass('dangerinput');
    }
    if (appDes == "") {
        $("#appDesc").addClass('dangerinput');
    }
    else {
        $("#appDesc").removeClass('dangerinput');
    }
}

function checkBtnTheme() {
    document.getElementById("btnTheme").style.color = "White";
    document.getElementById("btnTheme").style.backgroundColor = "Green";

}

function KeepCount() {
    var count = 0;
    if (document.getElementById("mainForm").elements["i1"].checked == true) {
        count = count + 1
    }
    if (document.getElementById("mainForm").elements["i2"].checked == true) {
        count = count + 1
    }
    if (document.getElementById("mainForm").elements["i3"].checked == true) {
        count = count + 1
    }
    if (document.getElementById("mainForm").elements["i4"].checked == true) {
        count = count + 1
    }
    if (document.getElementById("mainForm").elements["i5"].checked == true) {
        count = count + 1
    }
    if (document.getElementById("mainForm").elements["i6"].checked == true) {
        count = count + 1
    }
    if (count >= 1) {
        document.getElementById("lobApps").style.backgroundColor = "Green";
        document.getElementById("lobApps").style.color = "White";
    }
    else if (count == 0) {
        document.getElementById("lobApps").style.backgroundColor = "#007bff";
        document.getElementById("lobApps").style.color = "White";
    }
}

function setThemescolor() {
    if (!$("#FontFamily")[0].value.includes("Select") &&  !$("#fontcolor")[0].value.includes("Select"))  {
        document.getElementById("btnTheme").style.color = "White";
        document.getElementById("btnTheme").style.backgroundColor = "Green";
    }
    else if (!$("#FontFamily")[0].value.includes("Select") || !$("#fontcolor")[0].value.includes("Select")) {
        document.getElementById("btnTheme").style.color = "White";
        document.getElementById("btnTheme").style.backgroundColor = "Green";
    }
    else{
        document.getElementById("btnTheme").style.color = "White";
        document.getElementById("btnTheme").style.backgroundColor = "#007bff";
    
    }
}


