
function getSelectedLoanData() {
    var selectedLoanID = document.getElementById("loanTypeDropDownList").value;

    if (selectedLoanID != 0) {
        // get interest
        $.getJSON('../api/Loan/GetInterest', {
            LoanTypeID: selectedLoanID
        }).done(
            function (interest) {
                document.getElementById("interestField").value = interest.toLocaleString("en",
                    {
                        style: "percent"
                    });
            });

        // get total amount

        $.getJSON('../api/Loan/GetAmount', {
            LoanTypeID: selectedLoanID
        }).done(
            function (amount) {
                document.getElementById("totalAmountField").value = amount.toLocaleString("en",
                    {
                        style: 'currency',
                        currency: 'USD',
                    });
            });
    }
    else {
        document.getElementById("totalAmountField").value = "-";
        document.getElementById("interestField").value = "-";
    }
}

function calculateLoan() {
    var selectedLoanID = document.getElementById("loanTypeDropDownList").value;
    var years = document.getElementById("totalYearsField").value;

    $.getJSON('api/Loan/CalculatePayments',
        {
            LoanTypeID: selectedLoanID,
            NumberOfYears: years,
        }).done(
        function (data) {
            $('#calculationsTable').find("tr:gt(0)").remove(); // remove all table rows greater than index 0

            $(data).each(function (index, obj) {
                var row = document.getElementById("calculationsTable").insertRow();

                // number
                row.insertCell(0).innerHTML = index + 1;

                // total value
                row.insertCell(1).innerHTML = obj.Total.toLocaleString("en",
                    {
                        style: 'currency',
                        currency: 'USD',
                    });

                // capital
                row.insertCell(2).innerHTML = obj.Capital.toLocaleString("en",
                    {
                        style: 'currency',
                        currency: 'USD',
                    });

                // interest
                row.insertCell(3).innerHTML = obj.Interest.toLocaleString("en",
                    {
                        style: 'currency',
                        currency: 'USD',
                    });
            });
        });
}

// jQuery section

$(function () {
    $.getJSON('../api/Loan/GetLoanTypes').done(
        function (loanTypes) {
            var dropDownList = document.getElementById("loanTypeDropDownList");

            // loanTypes variable is an object list
            $(loanTypes).each(
                function (index, obj) {
                    var option = document.createElement('option');

                    option.text = (index + 1) + ". " + obj.LoanText;
                    option.value = obj.LoanTypeID;

                    dropDownList.appendChild(option);
                });
        });
});

$(function () {
    $("#loanTypeDropDownList, #totalYearsField").bind("change keyup",
        function () {
            if ($("#loanTypeDropDownList").val() != "" && $("#totalYearsField").val() != "") {
                document.getElementById("calculateButton").disabled = false;
            } else {
                document.getElementById("calculateButton").disabled = true;
            }
        });
});