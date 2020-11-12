# <h1>README</h1>
<h4>Binary Tree Manager Project</h4>

<br>

 ![alt text](https://github.com/Sagaracorp/BladeportBinaryTreeManager/blob/master/ReadMe/binarytree.png?raw=true)

<br>

<h5>Architecture</h5>

The tech stack used in the project is as follows:

1. .Net Core - Framework
2. C# - Backend
3. Angular - Front End
4. Microsoft SQL Server - Database
5. Swagger - API Documentation

<h5>Setup</h5>

1. Database

Run the following sql script file using <b>Microsoft SQL Server</b>. The following database objects should be created. 
Note that the name of the database should be <b>treemanagerdb</b>, else you will need to change the Entity Framework configuration on the .Net Core application.
The Forced Matrix table should be created as per requirement, the included scripts creates a 3x3 Froced Matrix. If you will be needing a Forced Matrix with 3x5 configuration then adjust the script as necessary. 

<p><b>IMPORTANT!</b> The User table is pre-populated by the script, it is assumed that these Users are already existing before inserting any Nodes to Binary Tree and Forced Matrix</p>
<br>
<p><b>IMPORTANT!</b> The table name format for Forced Matrix should be ForcedMatrix(X)x(Y), where X = Child Limit and Y = Level Limit e.g. ForcedMatrix3x5</p>

<br>

Link to [Database Scripts](https://github.com/Sagaracorp/BladeportBinaryTreeManager/blob/master/ReadMe/database_script.sql) here.

<br>

![alt text](https://github.com/Sagaracorp/BladeportBinaryTreeManager/blob/master/ReadMe/database.png?raw=true)

2. Backend 

Open BladeportBinaryTreeManager solution and compile using <b>Visual Studio 2019</b>. The Angular application is housed inside wwwroot folder of the <b>BladeportBinaryTreeManager.Web</b> project. 
  
<br>

![alt text](https://github.com/Sagaracorp/BladeportBinaryTreeManager/blob/master/ReadMe/angular.png?raw=true)

<br>

3. API 

The APIs are documented via swagger which can be accessed by <b>web address\index.html</b> e.g. https://localhost:44358/index.html on local.

<br>

![alt text](https://github.com/Sagaracorp/BladeportBinaryTreeManager/blob/master/ReadMe/swagger.png?raw=true)

<br>

You will be needing <b>Angular CLI</b> to execute the angular app locally via <b>ng serve</b> command. 
  
<br>

![alt text](https://github.com/Sagaracorp/BladeportBinaryTreeManager/blob/master/ReadMe/angularcli.png?raw=true)

<h5>Local Execution</h5>

1. Run the .Net Core application (F5) via Visual Studio 2019. The landing page should be the Swagger API documentation. 

2. Navigate to the Angular application BladeportBinaryTreeManager.Web/wwwrooot/app/ and execute <b>ng serve</b> command to compile and start dev server.

3. Angular CLI will expose a local server address, navigate to it and the landing page containing both Binary Tree and ForcedMatrix3x3 should display. No tree will dispay if you haven't inserted any nodes yet via API.

4. Insert Nodes by using the following parameters on Swagger:

  Binary Tree
  ![insert node binary tree](https://github.com/Sagaracorp/BladeportBinaryTreeManager/blob/master/ReadMe/swagger_insert_bt.png?raw=true)
  
  <br>
  
  Forced Matrix 
  ![insert node forced matrix](https://github.com/Sagaracorp/BladeportBinaryTreeManager/blob/master/ReadMe/swagger_insert_fm.png?raw=true)
  
  <br>
  
5. If you reached the limit for Forced Matrix, the 3x3 configuration should look like the tree below:

  <br>
  
   ![forced matrix](https://github.com/Sagaracorp/BladeportBinaryTreeManager/blob/master/ReadMe/forcedmatrix.png?raw=true)
   
  <br>
 
