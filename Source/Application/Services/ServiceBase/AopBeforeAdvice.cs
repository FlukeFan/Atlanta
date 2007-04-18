
using System.Reflection;

using Spring.Aop;

namespace Atlanta.Application.Services.ServiceBase
{

    /// <summary>
    ///  Class for applying 'before' AOP advice
    /// </summary>
    public class AopBeforeAdvice : IMethodBeforeAdvice
    {

        /// <summary>
        ///  Handle 'before' advice
        /// </summary>
        public void Before( MethodInfo  method,
                            object[]    args,
                            object      target)
        {
        }

    }

}

