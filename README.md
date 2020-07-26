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

4.生命周期管理   2020.7.25
        瞬时
        单例：声明一个字典或者其它类型来保存每个实例过的类型，每个来容器中请求对象的实例时，首先检测该类型在字典中是否实                  例化即可。
        作用域单例：作用域单例  就是Http请求时，一个请求处理过程中，创建得都是同一个实例，不同得请求处理过程中，创建得就                            是不同得实例。得区分请求，HTTP请求---AspNet.Core内置kestrel，初始化一个容器实例，然后每次来一个很                            Http请求，就clone一个，或者叫创建一个子容器{包含注册关系}，然后一个请求就一个子容器实例，那么就可以做                            到请求单例了（其实就是子容器单例）。
                            处理请求：
                            if (this.ZhaoxiContainerScopeDictionary.ContainsKey(key))
                            {
                                return this.ZhaoxiContainerScopeDictionary[key];
                            }
                            else
                            {
                                break;
                            }
        还有一个线程单例，不做探究
                           
5.AOP扩展   2020.7.26
         使用特性对AOP进行扩展 //1.能解决哪些方法不用AOP的问题//2.可以把切面逻辑转移到特性里面去
         然后使用core的套娃式管道对AOP进行处理 。
         特性的基类：
                           public abstract  class BaseInterceptorAttribute : Attribute
                          {
                               public abstract Action Do(IInvocation invocation, Action action);
        
                          }

          在AOP中处理如下：
                           Action action = () => base.PerformProceed(invocation);

                           if (method.IsDefined(typeof(BaseInterceptorAttribute), true))
                           {
                                foreach (var attribute in method.GetCustomAttributes<BaseInterceptorAttribute>().ToArray ().Reverse())
                                {
                                    action = attribute.Do(invocation,action);
                                }
                
                           }
                           //那就说明前面不能执行具体动作--前面只能是组装动作--配置管道模型--委托

                          action.Invoke();
6.AOP与IOC整合
          IOC容器在最后返回结果时使用AOP注入即可。
            
        
        
        
        
