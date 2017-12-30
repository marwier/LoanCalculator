
function getSelectedLoanData() {
    var selectedLoanId = document.getElementById("loanTypeDropDownListID").value;

    if (selectedLoanId !== "") {
        // get interest
        $.getJSON('../api/Loan/GetInterest', {
            LoanTypeId: selectedLoanId
        }).done(
            function (interest) {
                document.getElementById("interestFieldID").value = interest.toLocaleString("en",
                    {
                        style: "percent"
                    });
            });

        // get total amount

        $.getJSON('../api/Loan/GetAmount', {
            LoanTypeId: selectedLoanId
        }).done(
            function (amount) {
                document.getElementById("totalAmountFieldID").value = amount.toLocaleString("en",
                    {
                        style: 'currency',
                        currency: 'USD'
                    });
            });
    }
    else {
        document.getElementById("totalAmountFieldID").value = "-";
        document.getElementById("interestFieldID").value = "-";
    }
}

function calculateLoan() {
    var selectedLoanId = document.getElementById("loanTypeDropDownListID").value;
    var years = document.getElementById("totalYearsFieldID").value;

    // disable button while calculating
    var button = document.getElementById("calculateButtonID");
    button.firstChild.data = "Calculating...";
    button.disabled = true;

    // show loading circle
    var loadingObject = document.getElementById("loadingObject");
    loadingObject.hidden = false;

    // hide table
    var table = document.getElementById("calculationsTableID");
    table.hidden = true;

    // remove all table rows greater than index 0
    $("#calculationsTableID").find("tr:gt(0)").remove();

    $.getJSON('../api/Loan/ReturnPayments',
        {
            LoanTypeId: selectedLoanId,
            NumberOfYears: years
        }).done(
        function (data) {
            $(data).each(function (index, obj) {
                var row = document.getElementById("calculationsTableID").insertRow();

                // number
                row.insertCell(0).innerHTML = index + 1;

                // total value
                row.insertCell(1).innerHTML = obj.Total.toLocaleString("en",
                    {
                        style: 'currency',
                        currency: 'USD'
                    });

                // capital
                row.insertCell(2).innerHTML = obj.Capital.toLocaleString("en",
                    {
                        style: 'currency',
                        currency: 'USD'
                    });

                // interest
                row.insertCell(3).innerHTML = obj.Interest.toLocaleString("en",
                    {
                        style: 'currency',
                        currency: 'USD'
                    });
            });


        }).fail(function () {
            alert("Failed to calculate payments!\nPlease lower total years value!");
        }).always(function () {
            button.firstChild.data = "Calculate!";
            button.disabled = false;
            table.hidden = false;
            loadingObject.hidden = true;
        });
}

$(function () {
    $.getJSON('../api/Loan/GetLoanTypes').done(
        function (loanTypes) {
            var dropDownList = document.getElementById("loanTypeDropDownListID");

            // loanTypes variable is an object list
            $(loanTypes).each(
                function (index, obj) {
                    var option = document.createElement("option");

                    option.text = index + 1 + ". " + obj.LoanText;
                    option.value = obj.LoanTypeId;

                    dropDownList.appendChild(option);
                });

            document.getElementById("loadingPageContentID").hidden = true;
            document.getElementById("pageContentID").hidden = false;
        });
});

$(function () {
    $("#loanTypeDropDownListID, #totalYearsFieldID").bind("change keyup",
        function () {
            if ($("#loanTypeDropDownListID").val() !== "" && $("#totalYearsFieldID").val() !== "" &&
                !isNaN(parseInt($("#totalYearsFieldID").val())) && isFinite($("#totalYearsFieldID").val())) {
                document.getElementById("calculateButtonID").disabled = false;
            } else {
                document.getElementById("calculateButtonID").disabled = true;
            }
        });
});