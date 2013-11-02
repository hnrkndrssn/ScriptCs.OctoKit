ScriptCs.ScriptPackBoilerplate
==============================

A template for building [ScriptCS](https://github.com/scriptcs/scriptcs) script packs.

## Usage

### Update AssemblyInfo.cs

You should update the information inside of AssemblyInfo.cs to be based on the script pack you are creating. You are welcome to change all fields including the copyright.

### Update Namespace

Change the namespace inside ScriptPack.cs and BoilerplateContext.cs to something appropriate for your script pack.

### Rename BoilerplateContext

Rename BoilerplateContext.cs filename and class name to the name you want to be used when using this script pack. For example, as it is now you would execute this command to use this script pack:

    var context = Require<BoilerplateContext>();

### Update ScriptPack.cs

* Rename the class to the name you used in the previous step.
* Update the GetContext() method to return the object from the class you defined in the previous step.

### Implement your script pack functionality

Use the comments inside the two classes to help guide you on creating your script pack.

##License

The MIT License (MIT)

Copyright (c) 2013 Scott Smith

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.