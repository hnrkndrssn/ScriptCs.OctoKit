using System.Collections.Generic;
using ScriptCs.Contracts;

namespace ScriptCs.Octokit
{
    public class ScriptPack : IScriptPack
    {
        public IScriptPackContext GetContext()
        {
            //Return the ScriptPackContext to be used in your scripts
            return new OctokitPack();
        }

        public void Initialize(IScriptPackSession session)
        {

            //Here you can perform initialization like pass using statements 
            //and references by using the session object's two methods:

            //AddReference
            //Use this method to add library references that need to be 
            //available in your script. After the script pack is loaded, 
            //the specified references will be available for use 
            //without any code inside the script.
            session.AddReference("System.Net.Http.dll");

            //ImportNamespace
            //This method can import namespaces for use in your scripts to help 
            //keep user's scripts clean and simple.
            session.ImportNamespace("System.Net.Http.Headers");
            session.ImportNamespace("Octokit");
            session.ImportNamespace("ScriptCs.Contracts");
            session.ImportNamespace("ScriptCs.Octokit");
        }

        public void Terminate()
        {
            //Cleanup any resources here
        }
    }
}
