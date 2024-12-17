# 🚀 **FilterSharp**

**FilterSharp** is a powerful library for applying filtering, sorting, pagination, and change requests to `IQueryable` data sources in an efficient and secure manner.

---

## 📦 **Features**

- ✅ **Filtering**: Apply dynamic filters to your data queries.
- 🔄 **Sorting**: Sort data with multiple criteria.
- 📄 **Pagination**: Easily paginate large datasets.
- ⚙️ **Change Requests**: Handle and apply change requests dynamically.
- 🔒 **Security**: Manage query permissions with Fluent API and attribute-based configurations.

---

## 📥 **Installation**

Install the package via NuGet Package Manager:

```bash
Install-Package FilterSharp
```
---

##  ⚡ **Injecting FilterSharp** ##
```bash
        serviceCollection.AddFilterSharp(options =>
        { 
            options.DefaultPageSize = 10;
        });
```

### 🔹 **How to Use This**

## 🔄 Extending FilterSharp with Custom Mappers ##
You can extend FilterSharp by creating custom mappers for specific types. For example, you can create a UserFilterSharpMapper to customize filtering and sorting for a User entity.
```bash
public class UserFilterSharpMapper : AbstractFilterSharpMapper<User>
{
    public override void Configuration(FilterSharpMapperBuilder<User> builder)
    {
        builder.OnField(x => x.Name)
            .FilterFieldName("FirstName")
            .AllowedOperators([FilterOperator.Equals,FilterOperator.InRange])
            .DisableSort();
    }
}
```
OR

```code
public sealed class User
{
    public int Id { get; private set; }

    [FilterSharp(FilterFieldName = "FirstName",AllowedOperators = [FilterOperator.Equals,FilterOperator.Contains])]
    public string Name { get; private set; } 

```
---
## 🔒 **Security** ##

FilterSharp integrates security features allowing you to manage query permissions through Fluent API and attribute-based configurations. This ensures your data queries are protected from unauthorized access.

## 🛠️ **Usage** ##

**FilterSharp provides two main extension methods for applying queries to your data sources:**

ApplyQueryWithDetailsAsync – Returns data with additional metadata (e.g., total record count).
ApplyQueryAsResultAsync – Returns filtered data only, without additional metadata.

```bash
var data = await _dbContext.Users.ApplyQueryWithDetailsAsync(queryRequest); 
var data = await _dbContext.Users.ApplyQueryAsResultAsync(queryRequest);
```
 ## **Sample post** ##
```bash
POST /api/data-query HTTP/1.1
Host: your-api-url.com
Content-Type: application/json

{
  "filters": [
    {
      "field": "name",
      "operator": "contains",
      "value": "david"
    },
    {
      "field": "lastname",
      "operator": "equals",
      "value": "mrf"
    },
  ],
  "sorting": [
    {
      "filedName": "createdDate",
      "ascending": false
    }
  ],
  "pageNumber": 1,
  "pageSize": 10
}
```

**Supported Filter Operators in FilterRequest**
In DataQueryRequest under the FilterRequest class, the following filter operators are supported for the Operator property:

**contains**: Check if the value contains the specified string.
Example: Check if the string "Hello World" contains the word "World".

**notContains**: Check if the value does not contain the specified string.
Example: Check if the string "Hello World" does not contain the word "Goodbye".

**equals**: Check if the value is equal to the specified value.
Example: Check if the number 5 is equal to 5.

**notEqual**: Check if the value is not equal to the specified value.
Example: Check if the number 5 is not equal to 10.

**lessThan**: Check if the value is less than the specified value.
Example: Check if the number 3 is less than 5.

**lessThanOrEqual**: Check if the value is less than or equal to the specified value.
Example: Check if the number 5 is less than or equal to 5.

**greaterThan**: Check if the value is greater than the specified value.
Example: Check if the number 7 is greater than 5.

**greaterThanOrEqual**: Check if the value is greater than or equal to the specified value.
Example: Check if the number 6 is greater than or equal to 5.

**blank**: Check if the value is blank (null or empty).
Example: Check if a field in the database is blank.

**notBlank**: Check if the value is not blank.
Example: Check if a field in the database is not blank.

**inRange**: Check if the value is within a specified range.
Example: Check if the number 7 is within the range of 5 to 10.
For example:

Nested Filters Support:
Nested Filters: FilterSharp also supports nested filters, which allow you to build complex queries by combining multiple filter conditions.

Logical Operators:
The Logic property in FilterRequest determines how the conditions are combined. By default, it is set to AND, meaning all conditions must be true. However, you can override this in your request by setting it to OR.

AND: All conditions must be true.
OR: Any one of the conditions must be true.
These operators and logical combinations allow you to create dynamic and flexible queries for your data sources.

---



