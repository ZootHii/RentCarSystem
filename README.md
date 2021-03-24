<br/>
<p align="center">
  <h1 align="center">RentCarSystem</h1>
<p align="center">
  <h4 align="center">Layered Architecture Rent Car System Back-End.</h4>
    
 
## Built With
<ul>
  <li>C#</li>
  <li>EntityFramework</li>
  <li>Autofac (Aspect Orianted Programming, Dependency Injection)</li>
  <li>FleuntValidation</li>
  <li>IoC (Inversion of Control)</li>
  <li>Microsoft CacheMemory</li>
</ul>

## Specifications
<ul>
  <li>Security (Encryption, Hashing, TokenCreation (JWT : Json Web Token))</li>
  <li>Result (SuccessResult, ErrorResult, SuccessDataResult, ErrorDataResult)</li>
  <li>IoC (CoreModule : Dependency Resolving for core Microsoft, ServiceTool)</li>
  <li>Interceptor (Aspects Control)</li>
  <li>Files (FormFile, File operations)</li>
  <li>Aspects Attribute (Cache, Performance, Transaction, Validation)</li>
</ul>

## Tables
#### Cars

| Name        | Data Type     | Allow Nulls |
| :---------- | :------------ | :---------- |
| ID          | int           | NOT NULL    |
| BrandID     | int           | NOT NULL    |
| ColorID     | int           | NOT NULL    |
| ModelYear   | date          | NOT NULL    |
| DailyPrice  | money         | NOT NULL    |
| Description | varchar(50)   | True        |

#### CarImages

| Name       | Data Type      | Allow Nulls |
| :--------- | :------------- | :---------- |
| ID         | int            | NOT NULL    |
| CarID      | int            | NOT NULL    |
| ImageName  | varchar(100)   | NOT NULL    |
| UploadDate | datetime       | NOT NULL    |
| ImagePath  | varbinary(MAX) | NOT NULL    |

#### Brands

| Name      | Data Type    | Allow Nulls |
| :-------- | :----------- | :---------- |
| ID        | int          | NOT NULL    |
| BrandName | varchar(25)  | NOT NULL    |

#### Colors

| Name      | Data Type    | Allow Nulls |
| :-------- | :----------- | :---------- |
| ID        | int          | NOT NULL    |
| ColorName | varchar(25)  | NOT NULL    |

#### Customers

| Name        | Data Type   | Allow Nulls |
| :---------- | :---------- | :---------- |
| ID          | int         | NOT NULL    |
| UserID      | int         | NOT NULL    |
| CompanyName | varchar(25) | NOT NULL    |

#### Rentals

| Name       | Data Type | Allow Nulls |
| :--------- | :-------- | :---------- |
| ID         | int       | NOT NUL     |
| CarID      | int       | NOT NUL     |
| CustomerID | int       | NOT NUL     |
| RentDate   | datetime  | NOT NUL     |
| ReturnDate | datetime  | NULL        |

#### Users

| Name         | Data Type      | Allow Nulls |
| :----------- | :------------- | :---------- |
| ID           | int            | NOT NUL     |
| FirstName    | varchar(25)    | NOT NUL     |
| LastName     | varchar(25)    | NOT NUL     |
| EMail        | varchar(25)    | NOT NUL     |
| PasswordHash | varbinary(500) | NOT NUL     |
| PasswordSalt | varbinary(500) | NOT NUL     |
| Status       | bit            | NOT NUL     |

#### OperationClaims

| Name | Data Type    | Allow Nulls |
| :--- | :----------- | :---------- |
| ID   | int          | NOT NUL     |
| Name | varchar(250) | NOT NUL     |

#### UserOperationClaims

| Name             | Data Type | Allow Nulls |
| :--------------- | :-------- | :---------- |
| ID               | int       | NOT NUL     |
| UserID           | int       | NOT NUL     |
| OperationClaimID | int       | NOT NUL     |
