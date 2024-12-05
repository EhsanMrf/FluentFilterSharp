using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FilterSharp.Enum;

public enum FilterOperator
{
    [Display(Name = "contains")]
    Contains,
    [Display(Name = "notContains")]
    NotContains,
    [Display(Name = "equals")]
    Equals,
    [Display(Name = "notEqual")]
    NotEqual,
    [Display(Name = "lessThan")]
    LessThan,
    [Display(Name = "lessThanOrEqual")]
    LessThanOrEqual,
    [Display(Name = "greaterThan")]
    GreaterThan,
    [Display(Name = "greaterThanOrEqual")]
    GreaterThanOrEqual,
    [Display(Name = "blank")]
    Blank,
    [Display(Name = "notBlank")]
    NotBlank,
    [Display(Name = "inRange")]
    InRange
}