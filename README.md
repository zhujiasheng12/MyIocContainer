# MyIocContainer
IocContainer and Demo
此项目为自己手写的IOC容器，实现了构造注入，属性注入，方法注入，
大概实现的方法是在程序运行时，首先将对应的类型注册进一个字典中
private Dictionary<string, Type> ZhaoxiContainerDictionary = new Dictionary<string, Type>();
之后需要实例化接口时，从字典中拿取对应键的值即可

1.构造注入：
     构造注入可能出现多个构造函数的情况。解决方法是使用了一个特性
     [AttributeUsage (AttributeTargets.Constructor)]//标记构造函数注入
    public class ZhaoxiConstructorAttribute:Attribute
    {
    }
    将该特性标记到想要使用的构造函数上即可。
    然后容器类中增加了方法private string GetKey(string fullName,string shortName) => $"{fullName}___{shortName}";
    对同一接口的不同实例进行区分
    
    
    构造函数中可能出现需要多个没有注册的参数注入的情况，如下
    public TestServiceB(ITestServiceA testServiceA1, [ZhaoxiParameterConstant] string sIndex,ITestServiceA testServiceA2,[ZhaoxiParameterConstant]int iIndex)
        {
            _iIndex = iIndex;
            Console.WriteLine($"{this.GetType().Name} 被构造。。。！参数是：{sIndex}  {iIndex}");
        }
        解决方法是将需要的参数，比如[ZhaoxiParameterConstant] string sIndex与[ZhaoxiParameterConstant]int iIndex加上特性[ZhaoxiParameterConstant]，在注入的时候，检测方法参数的特性，然后注册的时候在字典 private Dictionary<string, object[]> ZhaoxiContainerValueDictionary = new Dictionary<string, object[]>();中事先注册好参数的值，然后按顺序取用即可。需要注意的是将参数区分开，集体区分代码如下：
        foreach (var para in ctor.GetParameters())//获取构造函数的参数
            {
                if (para.IsDefined(typeof(ZhaoxiParameterConstantAttribute), true))//此处找出特性ZhaoxiParameterConstantAttribute的参数
                {
                    paraList.Add(paraConstant[iIndex]);
                    iIndex++;
                }
                else//不然就按照正常注入即可
                {
                    Type paraType = para.ParameterType;//获取参数的类型 项目中是
                    string paraShortName = this.GetShortName(para);

                    object paraInstance = this.ResolveObject(paraType,paraShortName);
                    paraList.Add(paraInstance);//创建目标类型实例
                }
            }
            
            需要关注的是this.GetShortName方法。属性也调用了该方法
            private string GetShortName(ICustomAttributeProvider  provider)
        {
            if (provider.IsDefined(typeof(ZhaoxiParameterShortNameAttribute), true))
            {
                var attribute =(ZhaoxiParameterShortNameAttribute) (provider.GetCustomAttributes(typeof(ZhaoxiParameterShortNameAttribute), true)[0]);
                return attribute.ShortName;
            }
            else
                return null;
        }
        
2.属性注入
     使用特性[ZhaoxiPropertyInjectAttribute]标记属性
     
3.方法注入
     使用特性[ZhaoxiMethodAttribute]标记方法
        
        
        
        
