// class
function CustomerViewModel($scope, $http)  // $scope --> for scope management, $http --> --> to make a call to server
{
    // properties
    $scope.Customer =
        {
            "CustomerCode": "",
            "CustomerName": "",
            "CustomerAmount": "",
            "CustomerAmountColor": ""
        };

    $scope.Customers = {};

    $scope.$watch("Customers", function () {
        for (var x = 0; x < $scope.Customers.length; x++) {

            var cust = $scope.Customers[x];
            cust.CustomerAmountColor = $scope.getColor(cust.CustomerAmount);
        }
    })

    // function
    $scope.getColor = function (Amount) {
        if (Amount == 0) {
            return "";
        }

        if (Amount > 100) {
            return "Blue";
        }

        else {
            return "Red";
        }
    }

    $scope.$watch("Customer.CustomerAmount", function () {
        $scope.Customer.CustomerAmountColor = $scope.getColor($scope.Customer.CustomerAmount);
    });

    // make a call to server to insert data
    $scope.Add = function () {
        $http({ method: "POST", data: $scope.Customer, url: "Submit" }).then(function (successData, status, headers, config) {
            // Load the collection of Customer
            $scope.Customers = successData.data;

            // Clear text boxes
            $scope.Customer =
                {
                    "CustomerCode": "",
                    "CustomerName": "",
                    "CustomerAmount": "",
                    "CustomerAmountColor": ""
                };
        });
    }

    // Load Cutomer data for all customers
    $scope.Load = function () {
        $http({ method: "GET", url: "getCustomers" }).then(function (successData, status, headers, config) {
            // Load the collection of Customer
            $scope.Customers = successData.data;
        });
    }

    // Load Cutomer data for the entered Customer Name
    $scope.LoadByName = function () {

        var objCustomer = $scope.Customer;

        $http({ method: "GET", params: objCustomer, url: "getCustomerByName" }).then(function (successData, status, headers, config) {
            // Load the collection of Customer
            $scope.Customers = successData.data;
        });
        // use params to pass data to server in GET method OR use POST method
    }

    // call function to load Customer data
    $scope.Load();

}