# Mapping Generator Lean


"AutoMapper" like, Roslyn based, code fix provider that allows to generate mapping code in design time.

## Main features

### Generate mapping method body

#### Pure mapping method
Non-void method that takes single parameter

```csharp
public UserDTO Map(UserEntity entity)
{
    
}
```

![Generating pure mapping method implementation](doc/pure_mapping_method_newone.gif)


#### Updating method
Void method that takes two parameters
```csharp
public void Update(UserDTO source, UserEntity target)
{
    
}
```
![Generating update method implementation](doc/update_method.gif)


#### Mapping Constructor
Constructor method that takes single complex parameter

```csharp
public UserDTO(UserEntity user)
{
    
}
```
![Generating mapping constructor implementation](doc/mapping_constructor.gif)

Constructor method that takes more than one parameter

```csharp
public UserDTO(string firstName, string lastName, int age, int cash)
{
}
```

![Generating multi-parameter constructor](/doc/multiparameterconstructor.gif)



#### Updating member method
Void member method that takes single parameter
```csharp
public void UpdateWith(UserEntity en)
{
    
}
```

![Generating update member method imeplementation](doc/update_member_method.gif)

Void member method with more than one parameter
```csharp
public void Update(string firstName, string lastName, int age)
{
}
```
![](/doc/multiparameterupdate.gif)


### Generate inline code for fixing Compiler Errors: 
[CS0029](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/cs0029) Cannot implicitly convert type 'type' to 'type'

![cs0029](/doc/cs0029.jpg)

[CS0266](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/cs0266) Cannot implicitly convert type 'type1' to 'type2'. An explicit conversion exists (are you missing a cast?)

![cs0266](/doc/cs0266.jpg)

CS7036 There is no argument given that corresponds to the required formal parameter 

![CS7036](/doc/splatting.gif)

## Other mappings

- Complete empty initialization block
![Generate initialization bloc](doc/emptyInitialization.gif)

- Complete empty initialization block in lambda expression `Expression<Func<T,T2>> = (T) => new T2{}`
![initialization block in lambda expression](https://user-images.githubusercontent.com/7759991/41869113-4704c6f0-78b8-11e8-8c3c-47a6b5bf308c.gif)

- Provide local accessible variables as parameters for method and constructor invocation
![locals as parameters](doc/localsforconstructor.gif)

- Create missing mapping lambda  
![mapping lambda](doc/mapping_lambda.gif)

-  Generate ICloneable interface implementation
 ![generate clone method](https://user-images.githubusercontent.com/7759991/44867726-f45c0080-ac88-11e8-87e9-feed8242af79.gif)

## Object scaffolding

![sample scaffolding](/doc/object_scaffolding.gif)

## Mapping rules
- Mapping Property-To-Property
  ```csharp
  target.FirstName = source.FirstName;
  target.LastName = source.LastName;
  ```
- Mapping Method Call-To-Property
  ```csharp
  target.Total = source.GetTotal()
  ```
- Flattening with sub-property
  ```csharp
  target.UnitId = source.Unit.Id
  ```
- Mapping complex property
  ```csharp
   target.MainAddress = new AddressDTO(){
  	BuildingNo = source.MainAddress.BuildingNo,
  	City = source.MainAddress.City,
  	FlatNo = source.MainAddress.FlatNo,
  	Street = source.MainAddress.Street,
  	ZipCode = source.MainAddress.ZipCode
  };
  ```
- Mapping collections
  ```csharp
  target.Addresses = source.Addresses.Select(sourceAddresse => new AddressDTO(){
    BuildingNo = sourceAddresse.BuildingNo,
    City = sourceAddresse.City,
    FlatNo = sourceAddresse.FlatNo,
    Street = sourceAddresse.Street,
    ZipCode = sourceAddresse.ZipCode
  }).ToList().AsReadOnly();
  ```
- Unwrapping wrappers 
  ```csharp
  customerEntity.Kind = cutomerDTO.Kind.Selected;
  ```
  
  ```csharp
    public enum CustomerKind
    {
        Regular,
        Premium
    }
  
    public class Dropdown<T>
    {
        public List<T> AllOptions { get; set; }
  
        public T Selected { get; set; }
    }
  
    public class CustomerDTO
    {
        public string Name { get; set; }
        public Dropdown<CustomerKind> Kind { get; set; }
    }
  
    public class UserEntity
    {
        public string Name { get; set; }
        public CustomerKind Kind { get; set; }
    }
  ```
- Using existing direct mapping constructor
  ```csharp
  target.MainAddress = new AddressDTO(source.MainAddress);
  ```

- using existing multi-parameter constuctor
  ```csharp
  this.User =  new UserDTO(firstName: entity.FirstName, lastName: entity.LastName, age: entity.Age);
  ```

