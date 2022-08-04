using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using DesafioTakeAPI_1.API;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DesafioTakeAPI_1
{
    public class Application
    {

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        /// 


        public APIGatewayProxyResponse getRepositoryData(ILambdaContext context)
        {
            return new APIController().returnRepositoryData(context);
        }
        //public string FunctionHandler(string input, ILambdaContext context)
        //{
        //    APIGatewayProxyResponse test = getRepositoryData(context);
        //    return input?.ToUpper();
        //}
    }
}
