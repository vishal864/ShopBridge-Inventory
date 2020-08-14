# ShopBridge-Inventory

ShopBridge Project contains 6 layers :-
ShopBridge.API,
ShopBridge.UI,
ShopBridge.Repository,
ShopBridge.Service,
ShopBridge.Model,
ShopBridge.UnitTest

ShopBridge.API -> API layer is used for API call to service layer.
ShopBridge.UI -> UI layer is responsible for UI related modules.
ShopBridge.Repository -> It's a DAL which will be responsible for inventory CRUD operation.
ShopBridge.Service -> It's a BL which will be responsible for interacting with DAL layer.
ShopBridge.UnitTest -> It's Unit test project will be responsible for taking care of unit testing.
ShopBridge.Model -> Entities will be placed in this layer.

Open the solution ->

Set ShopBridge.UI project as start-up project. 

Befor running the solution follow below steps :-

Open ShopBridge.UI Web.config file and look for the ApiBaseAddress key and chnage its value accordindly.

<add key="ApiBaseAddress" value="http://localhost:50347/api/" />

You can keep as it is or change it accordingly.

Now open ShopBridge.Repository App.Config file and look for the connectionString section and change the Data Source as yours.

Now Build the solution and run.

Here, I have used code first approach.
So after running the solution below objects will be created in the Sql Server.
ShopBridge (Database)
Inventory(Table)
