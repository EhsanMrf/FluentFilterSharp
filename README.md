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

##  ⚡ ** Injecting FilterSharp** ##
```bash
services.AddFilterSharp();
```

### 🔹 **How to Use This**

## 🛠️ **Usage** ##
FilterSharp provides two main extension methods for applying queries to your data sources:

ApplyQueryWithDetailsAsync – Returns data with additional metadata (e.g., total record count).
ApplyQueryAsResultAsync – Returns filtered data only, without additional metadata.

🛠️ How to Use
Supported Filter Operators in FilterRequest
In DataQueryRequest under the FilterRequest class, the following filter operators are supported for the Operator property:

contains: Check if the value contains the specified string.
notContains: Check if the value does not contain the specified string.
equals: Check if the value is equal to the specified value.
notEqual: Check if the value is not equal to the specified value.
lessThan: Check if the value is less than the specified value.
lessThanOrEqual: Check if the value is less than or equal to the specified value.
greaterThan: Check if the value is greater than the specified value.
greaterThanOrEqual: Check if the value is greater than or equal to the specified value.
blank: Check if the value is blank (null or empty).
notBlank: Check if the value is not blank.
inRange: Check if the value is within a specified range.
These operators can be used to build powerful and dynamic queries for your data sources.

Nested Filters Support
FilterSharp also supports nested filters. You can combine multiple filter conditions using logical operators.

Logical Operators
The Logic property in FilterRequest determines how the conditions are combined. By default, it is set to AND, meaning all conditions must be true. However, you can override this in your request by setting it to OR.

For example:

AND (default): Ensures that all conditions must be true.
OR: Ensures that at least one condition must be true.


```bash
var data = await _dbContext.Users.ApplyQueryWithDetailsAsync(queryRequest); 
var data = await _dbContext.Users.ApplyQueryAsResultAsync(queryRequest);
```
---
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


