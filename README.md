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


