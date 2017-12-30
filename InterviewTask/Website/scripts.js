
function getSelectedLoanData() {
    var selectedLoanId = document.getElementById("loanTypeDropDownList").value;

    if (selectedLoanId !== "") {
        // get interest
        $.getJSON('../api/Loan/GetInterest', {
            LoanTypeId: selectedLoanId
        }).done(
            function (interest) {
                document.getElementById("interestField").value = interest.toLocaleString("en",
                    {
                        style: "percent"
                    });
            });

        // get total amount

        $.getJSON('../api/Loan/GetAmount', {
            LoanTypeId: selectedLoanId
        }).done(
            function (amount) {
                document.getElementById("totalAmountField").value = amount.toLocaleString("en",
                    {
                        style: 'currency',
                        currency: 'USD'
                    });
            });
    }
    else {
        document.getElementById("totalAmountField").value = "-";
        document.getElementById("interestField").value = "-";
    }
}

function calculateLoan() {
    var selectedLoanId = document.getElementById("loanTypeDropDownList").value;
    var years = document.getElementById("totalYearsField").value;

    $("#calculationsTable").find("tr:gt(0)").remove(); // remove all table rows greater than index 0

    // disable button while calculating
    var button = document.getElementById("calculateButton");
    button.firstChild.data = "Calculating...";
    button.disabled = true;


    $.getJSON('../api/Loan/ReturnPayments',
        {
            LoanTypeId: selectedLoanId,
            NumberOfYears: years
        }).done(
        function (data) {
            $(data).each(function (index, obj) {
                var row = document.getElementById("calculationsTable").insertRow();

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

            button.firstChild.data = "Calculate!";
            button.disabled = false;
        });
}

$(function () {
    $.getJSON('../api/Loan/GetLoanTypes').done(
        function (loanTypes) {
            var dropDownList = document.getElementById("loanTypeDropDownList");

            // loanTypes variable is an object list
            $(loanTypes).each(
                function (index, obj) {
                    var option = document.createElement('option');

                    option.text = (index + 1) + ". " + obj.LoanText;
                    option.value = obj.LoanTypeId;

                    dropDownList.appendChild(option);
                });
        });
});

$(function () {
    $("#loanTypeDropDownList, #totalYearsField").bind("change keyup",
        function () {
            if ($("#loanTypeDropDownList").val() !== "" && $("#totalYearsField").val() !== "" &&
                !isNaN(parseInt($("#totalYearsField").val())) && isFinite($("#totalYearsField").val())) {
                document.getElementById("calculateButton").disabled = false;
            } else {
                document.getElementById("calculateButton").disabled = true;
            }
        });
});