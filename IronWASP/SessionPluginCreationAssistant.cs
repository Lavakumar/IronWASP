//
// Copyright 2011-2013 Lavakumar Kuppan
//
// This file is part of IronWASP
//
// IronWASP is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, version 3 of the License.
//
// IronWASP is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with IronWASP.  If not, see http://www.gnu.org/licenses/.
//

using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace IronWASP
{
    public partial class SessionPluginCreationAssistant : Form
    {
        
        string CurrentQuestionType = "";
        
        const string QuestionTypeTextAnswer = "QuestionTypeTextAnswer";
        const string QuestionTypeRequestSourceAnswer = "QuestionTypeRequestSourceAnswer";
        const string QuestionTypeParametersAnswer = "QuestionTypeParametersAnswer";
        const string QuestionTypeResponseSignatureAnswer = "QuestionTypeResponseSignatureAnswer";
        const string QuestionTypeSelectOptionAnswer = "QuestionTypeSelectOptionAnswer";
        const string QuestionTypeShowPseudoCode = "QuestionTypeShowPseudoCode";

        bool ParametersAnswerAskUserOnly = false;
        bool ResponseSignatureFullResponse = false;

        Dictionary<string, Dictionary<string, string>> RequestDeclarationsInCode = new Dictionary<string, Dictionary<string, string>>();

        string PluginName = "";
        string PluginLang = "py";

        List<string> RequestNamesFromUserList = new List<string>();

        //Follow Redirect
        string FollowRedirectPseudoCode = "";

        //Update CSRF Token
        string UpdateCSRFTokensPseudoCode = "";

        //Handling Set-Cookies
        List<string> NamesOfCookieParametersToUpdate = new List<string>();

        //Handle Login
        string LoginActionPseudoCode = "";
        string LoginRequestSourcePseudoCode = "";
        //Logout Detection

        //Multi-step Form Submission
        string MultiStepActionPseudoCode = "";
        bool PreInjectionStepsPresent = false;
        bool PostInjectionStepsPresent = false;
        int PreInjectionCounter = 0;
        int PostInjectionCounter = 0;
        int PreInjectionCounterMax = 0;
        
        //Normal Response Signature

        const string HomeMenu = "HomeMenu";
        const string HandleRedirect = "HandleRedirect";
        const string HandleCSRFTokens = "HandleCSRFTokens";
        const string HandleEncodedParameters = "HandleEncodedParameters";
        const string HandleLogin = "HandleLogin";
        const string HandleMultiStep = "HandleMultiStep";
        const string HandleSetCookies = "HandleSetCookies";
        const string PluginCreation = "PluginCreation";

        const string LoginCheckLabel = "login_check";
        const string PerformLoginLabel = "do_login";
        const string FollowRedirectLabel = "follow_redirect";
        const string UpdateCsrfLabel = "update_tokens";
        const string MultiStepPreLabel = "multi_step_pre";
        const string MultiStepPostLabel = "multi_step_post";

        Dictionary<string, List<string>> ParametersToUpdate = new Dictionary<string, List<string>>();
        Dictionary<int, string> LogSources = new Dictionary<int, string>();

        #region Steps
        //HomeMenu
        internal const string HomeMenu__BaseStep = "HomeMenuBaseStep";

        //HandleRedirect
        //Ask use if he wants to specify or use pseudo code or go back
        internal const string HandleRedirect__BaseStep = "HandleRedirectBaseStep";
        //If user selected pseudo code then ask user for the pseudo code
        internal const string HandleRedirect__EnterPseudoCodeStep = "HandleRedirectEnterPseudoCodeStep";
        //Ask user to enter the signature for the redirect
        internal const string HandleRedirect__ResponseSignatureStep = "HandleRedirectResponseSignatureStep";
        //Display Pseudo Code to the user
        internal const string HandleRedirect__ShowPseudoCodeStep = "HandleRedirectShowPseudoCodeStop";

        //HandleSetCookies
        //Ask user if he wants to update SetCookies from response
        internal const string HandleSetCookies__BaseStep = "HandleSetCookiesBaseStep";
        //Ask the user to names of all cookie parameters that must be updated from set-cookies, one line at a time
        internal const string HandleSetCookies__ParameterNames = "HandleSetCookiesParameterNames";

        //HandleCSRFToken
        //Ask user if he wants to update CSRF token or use pseudo code or go back
        internal const string HandleCSRFTokens__BaseStep = "HandleCSRFTokensBaseStep";
        //if user selected pseudo code then ask user for the pseudo code
        internal const string HandleCSRFTokens__PseudoCodeStep = "HandleCSRFTokensPseudoCodeStep";
        //Ask user what will be the source of the parameter
        internal const string HandleCSRFTokens__ParameterSourceStep = "HandleCSRFTokensParameterSourceStep";               
        //Ask user for the source of the reques that must be sent to get the html containing values to update
        internal const string HandleCSRFTokens__RequestToSendForResponseStep = "HandleCSRFTokensRequestToSendForResponseStep";
        //Ask user for the parameter details
        internal const string HandleCSRFTokens__ParametersStep = "HandleCSRFTokensParametersStep";
        //Ask user for the parameter details
        internal const string HandleCSRFTokens__ParametersAskUserOnlyStep = "HandleCSRFTokens__ParametersAskUserOnlyStep";
        //Display the Psedudo Code to the user
        internal const string HandleCSRFTokens__ShowPseudoCodeStep = "HandleCSRFTokensShowPseudoCodeStep";


        //HandleLogin
        //Ask user if he wants to update CSRF token or use pseudo code or go back
        internal const string HandleLogin__BaseStep = "HandleLoginBaseStep";
        //if user selected pseudo code then ask user for the pseudo code
        internal const string HandleLogin__PseudoCodeStep = "HandleLoginPseudoCodeStep";
        //Ask user for the source of the Request to check if user of logged in
        internal const string HandleLogin__LoggedinCheckRequestSourceStep = "HandleLoginLoggedinCheckRequestSourceStep";
        //Ask user which section of the response must be checked to identify logged in or logged out
        internal const string HandleLogin__LoggedInLoggedOutSignatureResponseSectionStep = "HandleLoginLoggedInLoggedOutSignatureResponseSectionStep";
        //Ask user for the source of the login request
        internal const string HandleLogin__LoginRequestSourceStep = "HandleLoginLoginRequestSourceStep";
        //Ask user if any parameters of the the login request needs to be updated from the response of another request
        internal const string HandleLogin__ShouldUpdateLoginRequestStep = "HandleLoginShouldUpdateLoginRequestStep";
        //Ask user if another request must be sent to update the parameters of Login Request
        internal const string HandleLogin__IsPreLoginRequestNeededStep = "HandleLoginIsPreLoginRequestNeededStep";
        //Ask user for the source of the pre-login request
        internal const string HandleLogin__PreLoginRequestSourceStep = "HandleLoginPreLoginRequestSourceStep";
        //Ask user for parameters that must be updated in the login request
        internal const string HandleLogin__LoginRequestParametersStep = "HandleLoginLoginRequestParametersStep";
        //Ask user for parameters that must be updated in the to inject request from the login response
        internal const string HandleLogin__ToInjectRequestParametersStep = "HandleLoginToInjectRequestParametersStep";
        //Display the Psedudo Code to the user
        internal const string HandleLogin__ShowPseudoCodeStep = "HandleLoginShowPseudoCodeStep";


        //Handle MultiStep Form Submission
        //Ask user if he wants to perform MultiStep submission
        internal const string HandleMultiStep__BaseStep = "HandleMultiStepBaseStep";
        //if user selected pseudo code then ask user for the pseudo code
        internal const string HandleMultiStep__PseudoCodeStep = "HandleMultiStepPseudoCodeStep";
        //Ask user if actions must be performed both before and after injection
        internal const string HandleMultiStep__BeforeAfterInjectionSelectionStep = "HandleMultiStepBeforeAfterInjectionSelectionStep";
        //Ask user for the source of the request to be sent before injection
        internal const string HandleMultiStep__PreInjectionRequestSourceStep = "HandleMultiStepPreInjectionRequestSourceStep";
        //Ask user for the parameters of current request that must be updated from previous response
        internal const string HandleMultiStep__PreInjectionRequestParametersStep = "HandleMultiStepPreInjectionRequestParametersStep";
        //Ask user if another request must be sent before injection
        internal const string HandleMultiStep__MorePreInjectionRequestStep = "HandleMultiStepMorePreInjectionRequestStep";
        //Ask user for the parameters of the toinject request that must be updated from previous response
        internal const string HandleMultiStep__ToInjectRequestParametersStep = "HandleMultiStepToInjectRequestParametersStep";
        //Ask user for the source of the request to be sent after injection
        internal const string HandleMultiStep__PostInjectionRequestSourceStep = "HandleMultiStepPostInjectionRequestSourceStep";
        //Ask user for the parameters of current request that must be updated from previous response
        internal const string HandleMultiStep__PostInjectionRequestParametersStep = "HandleMultiStepPostInjectionRequestParametersStep";
        //Ask user if another request must be sent after injection
        internal const string HandleMultiStep__MorePostInjectionRequestStep = "HandleMultiStepMorePostInjectionRequestStep";
        //Display the Psedudo Code to the user
        internal const string HandleMultiStep__ShowPseudoCodeStep = "HandleMultiStepShowPseudoCodeStep";

        //PluginCreation
        //Ask user for the name of the plugin
        internal const string PluginCreation__PluginNameStep = "PluginCreationPluginNameStep";
        //Ask user which language the plugin must be created in
        internal const string PluginCreation__LanguageStep = "PluginCreationLanguageStep";
        //Final step
        internal const string PluginCreation__FinalStep = "PluginCreationFinalStep";

        #endregion


        #region Questions
        
        static string GenericQuestionToSpecifyRequestSource = @"{0}
<i<br>><i<br>>
An existing request from one of the logs can be picked.
<i<br>><i<br>>
Specify the Request to pick by mentioning the Log Source and the ID number of the Request inside the log.
<i<br>><i<br>>
Also mention a name to this request, this name will be used to refer to this request in the SessionPlugin trace messages.
<i<br>><i<br>>
";


        static string GenericQuestionToShowPseudoCode = @"{0}
The answers provided by you will be used to generate the Session Plugin.
<i<br>><i<br>>
If you wish to provide the same set of answers again then you don't have to answer the all the questions in the UI.
<i<br>><i<br>>
Instead just provide the Pseudo Code that is displayed below.
<i<br>><i<br>>
The Pseudo Code is an exact representation of your answers and will be used for making the Session Plugin.
<i<br>><i<br>>
Please go back to the main menu answer further questions or to create a Session Plugin based on your answers.
<i<br>><i<br>>
";

        static Dictionary<string, string> Questions = new Dictionary<string, string>() {
        //Home Menu
        {HomeMenu__BaseStep, @"
<i<hh>>Session Plugin Creation Assistant - Main Menu<i</hh>>
<i<br>><i<br>>
This assistant will help you create a Session Plugin that can handle custom site behaviours like login, csrf token update etc.
<i<br>><i<br>>
This session plugin can be used in your Automated Scan Jobs, Log Tester, Fuzzers or any other tools that support them.
<i<br>><i<br>>
Depending on which action you want to program you can select an option from below. Selecting the option will show more details about it.
<i<br>><i<br>>
Once you have defined how any one or more of these actions must be performed you can create a Session Plugin by clicking the appropriate button below.
<i<br>><i<br>>
"},
        //Handle Redirect
        {HandleRedirect__BaseStep, @"
<i<hh>>Follow Responses with Redirect<i</hh>>
<i<br>><i<br>>
When the request that you are scanning/testing returns a response which is a 301 or 302 redirect to another location then you could choose to follow that response.
<i<br>><i<br>>
This can be handy in scenarios where the application always returns a redirect for a request and following the redirect will display the actual result of the first request.
<i<br>><i<br>>
In such cases it would be better if the scanner or the testing tool performed its analysis on the response got after following the redirect.
<i<br>><i<br>>
If you wish to add this action to the Session Plugin then select 'Yes' below and hit 'Submit'.
<i<br>><i<br>>
If you have already defined this action and have corresponding Pseudo Code that you would like to provide then select the 'Pseudo Code' option below.
<i<br>><i<br>>
"},
        {HandleRedirect__ResponseSignatureStep ,@"<i<hh>>Redirect Signature Definition<i</hh>>
<i<br>><i<br>>
You can define when a redirect must be followed by specifying the signature of the response that you would like to be followed.
<i<br>><i<br>>
The signature is defined by specifying the response code and optionally the value of the location header.
<i<br>><i<br>>
If you don't define a signature then all redirects will be followed.
<i<br>><i<br>>
"},
        {HandleRedirect__EnterPseudoCodeStep, @"<i<hh>>Follow Redirect Pseudo Code<i</hh>>
<i<br>><i<br>>
If you have already defined the follow redirect action then enter the corresponding pseudo code below.
<i<br>><i<br>>
"},
        {HandleRedirect__ShowPseudoCodeStep, string.Format(GenericQuestionToShowPseudoCode, @"<i<hh>>Pseudo Code for Handle Redirect Action<i</hh>>
<i<br>><i<br>>")},
        // Handle SetCookies
        {HandleSetCookies__BaseStep, @"<i<hh>>Read Set-Cookies from Response<i</hh>>
<i<br>><i<br>>
If the response recieved after sending the main request being scanned or tested has Set-Cookie headers then you can define if these values must be added to the cookie of the next main request that is sent.
<i<br>><i<br>>
Only those Set-Cookie values whose names will be mentioned will be updated in the Cookie header of the next main request.
<i<br>><i<br>>
If you wish you specify the Set-Cookie values then selected 'Yes' and hit 'Submit'
<i<br>><i<br>>
        "},
        {HandleSetCookies__ParameterNames, @"<i<hh>>Set-Cookies Names<i</hh>>
<i<br>><i<br>>
Enter the names of the Set-Cookie headers that you want to be updated in the Cookie header of the next main request.
<i<br>><i<br>>
Enter the names one per line.
<i<br>><i<br>>
"},
        //Handle CSRF Tokens
        {HandleCSRFTokens__BaseStep, @"<i<hh>>CSRF-token updating<i</hh>>
<i<br>><i<br>>
Some requests might have a CSRF token parameter whose value needs to be updated everytime after it is sent or if the underlying session id changes.
<i<br>><i<br>>
If the main request being scanned/tested has such a parameter or any other parameter that needs to be updated before it is sent then you can define that action in this section.
<i<br>><i<br>>
If you wish you define this action then select 'Yes' and hit the 'Submit' button.
<i<br>><i<br>>
If you have already defined this action and have the corresponding Pseudo Code and you like to use the same then selected the 'Pseudo Code' option.
<i<br>><i<br>>
        "},
        {HandleCSRFTokens__PseudoCodeStep, @"<i<hh>>CSRF-token Update Pseudo Code<i</hh>>
<i<br>><i<br>>
If you have already defined the csrf-token update action then enter the corresponding pseudo code below.
<i<br>><i<br>>
"},
        {HandleCSRFTokens__ParameterSourceStep, @"<i<hh>>CSRF-token Source Definition<i</hh>>
<i<br>><i<br>>
Before the CSRF-token or any other value can be updated in the request we must get it from some source.
<i<br>><i<br>>
There are two supported sources:
<i<br>><i<br>>
1) Send another request and extract the values from the response of this request
<i<br>><i<br>>
2) During the scan or test display a pop-up box asking the user to enter this value. (Useful for entering RSA tokens or solving captchas)
<i<br>><i<br>>
<i<br>><i<br>>
Depending on from where you would like to get the token value, select an option below.
<i<br>><i<br>>
Please note that when you select the first option in addition to extract some values from the response of another request, you can also choose to update some parameters by prompting the user.
<i<br>><i<br>>
        "},
        {HandleCSRFTokens__RequestToSendForResponseStep, string.Format(GenericQuestionToSpecifyRequestSource, @"<i<hh>>Request to send to get CSRF-token<i</hh>>
<i<br>><i<br>>
You have chose to send a request and extract the token from the response. Enter the details of the request that must be used for this purpose.
<i<br>><i<br>>
")},
        {HandleCSRFTokens__ParametersStep, @"<i<hh>>'{0}' CSRF-token Parameter Update Definition<i</hh>>
<i<br>><i<br>>
The parameters of the '{0}' will be updated based on your requirements.
<i<br>><i<br>>
You must first specify the section of the request the parameter belongs to like Query, Body etc and  the name of the parameter. This way the parameter to update can be identified.
<i<br>><i<br>>
If you want to update the path section of the URL incase the server is using URL Rewriting then select 'UrlPathPart' and give the zero-based index of the path section you want to update.
<i<br>><i<br>>
Then the value to update this parameter with must be made ready. If the value is being extracted from a response of the '{1}' request then you must specify how this value can be extracted.
<i<br>><i<br>>
If this value occurs inside the value attribute of input tags in the response then it is taken from the first input tag that has the same name as the parameter name provided earlier.
<i<br>><i<br>>
If this value occurs in a different place then you can provide a regex to extract this value. This regex will be used on the entire response body and the first match will be used.
<i<br>><i<br>>
For example if the token appears inside the SCRIPT tag of the response as below:
<i<br>><i<br>>
var c_token = 'ksd9283-asdjsd023-askjd23';
<i<br>><i<br>>
Then the regex to extract this token would be: var c_token = '(.*?)';
<i<br>><i<br>>
If you would want to get the value of this parameter from the user by throwing a prompt then you can select that option and provide a hint that will be included in the prompt to help the user identify the correct parameter.
<i<br>><i<br>>
You can choose to update multiple parameters in this section. After you are done defining the parameters and the parameters are listed in the table at the bottom you can submit this definition.
<i<br>><i<br>>
"},
        {HandleCSRFTokens__ParametersAskUserOnlyStep, @"<i<hh>>'{0}' CSRF-token Parameter Update Definition<i</hh>>
<i<br>><i<br>>
The parameters of the '{0}' will be updated based on your requirements.
<i<br>><i<br>>
You must first specify the section of the request the parameter belongs to like Query, Body etc and  the name of the parameter. This way the parameter to update can be identified.
<i<br>><i<br>>
If you want to update the path section of the URL incase the server is using URL Rewriting then select 'UrlPathPart' and give the zero-based index of the path section you want to update.
<i<br>><i<br>>
The value of this parameter will be got from the user by throwing a prompt, provide a hint that will be included in the prompt to help the user identify the correct parameter.
<i<br>><i<br>>
You can choose to update multiple parameters in this section. After you are done defining the parameters and the parameters are listed in the table at the bottom you can submit this definition.
<i<br>><i<br>>
"},
        {HandleCSRFTokens__ShowPseudoCodeStep, string.Format(GenericQuestionToShowPseudoCode, @"<i<hh>>Pseudo Code for Handle CSRF-tokens Action<i</hh>>
<i<br>><i<br>>")},
        //Handle Login
        {HandleLogin__BaseStep, @"<i<hh>>Handle Login<i</hh>>
<i<br>><i<br>>
If you are scanning or testing a request that belongs to the post-authentication section of the site then it is important to check if the user is still logged in and if the user is logged out a proper login must be performed and the new session information must be updated in the main request before it is sent to the server.
<i<br>><i<br>>
This section will let you define how to check if the user is still logged in and also to perform a proper login and updating of new session information.
<i<br>><i<br>>
If you wish you define this action then select 'Yes' and hit the 'Submit' button.
<i<br>><i<br>>
If you have already defined this action and have the corresponding Pseudo Code and you like to use the same then selected the 'Pseudo Code' option.
<i<br>><i<br>>
"},
        {HandleLogin__PseudoCodeStep, @"<i<hh>>Handle Login Pseudo Code<i</hh>>
<i<br>><i<br>>
If you have already defined the handle login action then enter the corresponding pseudo code below.
<i<br>><i<br>>
"},
        {HandleLogin__LoggedinCheckRequestSourceStep, string.Format(GenericQuestionToSpecifyRequestSource, @"<i<hh>>Request to send to check Logged in status<i</hh>>
<i<br>><i<br>>
A simple request must be sent to check if the user is still logged in. This could be the request to the user's profile page or welcome page depending on the site's design.
<i<br>><i<br>>
In any case this must be a request that gives two different responses based on the logged in status of the user so that the response can be analyzed to determine this.
<i<br>><i<br>>
Specify such a request which will used for this purpose. This request will be referred to as the 'Login Check Request' in subsequent sections.
<i<br>><i<br>>
")},
        {HandleLogin__LoggedInLoggedOutSignatureResponseSectionStep, @"<i<hh>>Analyze response of the 'Login Check Request'<i</hh>>
<i<br>><i<br>>
Once the 'Login Check Request' is sent and a response it recieved it must be analyzed to determine the current login status.
<i<br>><i<br>>
Since the response will be different for logged in and logged out status you can provide a signature for either one of those statuses.
<i<br>><i<br>>
Choose the one status that is easier to define using the options below. The options for analysis include checking the response code, response title, response body and/or response redirect location (in case of a redirect).
<i<br>><i<br>>
For example if the response would be a 200 for logged in status and a 302 to login.php for logged out status then it would suffice to select the 'Logged Out Signature' option and enter the status code as 302.
<i<br>><i<br>>
But if the differnce is not that obvious then you can make use of the other options to provide a reliable signature.
<i<br>><i<br>>
"},
        {HandleLogin__LoginRequestSourceStep, string.Format(GenericQuestionToSpecifyRequestSource, @"<i<hh>>Request to send to perform login<i</hh>>
<i<br>><i<br>>
If the analysis of the 'Login Check Request' shows that the user is logged out then a login must be performed.
<i<br>><i<br>>
This is done by sending a request that has the user's authentication credentials (username, password, rsa tokens etc).
<i<br>><i<br>>
Specify this request. This request will be referred to as the 'Login Request' in subsequent sections.
<i<br>><i<br>>
")},
         {HandleLogin__ShouldUpdateLoginRequestStep, @"<i<hh>>Updating the 'Login Request'<i</hh>>
<i<br>><i<br>>
If the 'Login Request' contains any parameters that need to be updated before sending it then it can be handled.
<i<br>><i<br>>
These could be CSRF-tokens in the 'Login Request' or one-time passwords like RSA tokens that must be entered by the user exactly at the time of login.
<i<br>><i<br>>
If such updates need to be performed on the 'Login Request' then select 'Yes' and hit 'Submit'.
<i<br>><i<br>>
        "},
        {HandleLogin__IsPreLoginRequestNeededStep,  @"<i<hh>>Should a 'Pre Login Request' be sent<i</hh>>
<i<br>><i<br>>
You have chose to update some of the parameters of the 'Login Request' before it is sent.
<i<br>><i<br>>
If any of these parameters need to be updated with values from the response of another request then it can be done. This is a typical requirement in case of CSRF-tokens.
<i<br>><i<br>>
If another request must be sent to extract parameter value from the response and use in the 'Login Request' then select 'Yes' and hit 'Submit'
<i<br>><i<br>>
"},
        {HandleLogin__PreLoginRequestSourceStep, string.Format(GenericQuestionToSpecifyRequestSource, @"<i<hh>>Request to send before sending the 'Login Request'<i</hh>>
<i<br>><i<br>>
You have chose to send a request before sending the 'Login Reqeust' and extract some parameter value from the response and use it to update the 'Login Request'.
<i<br>><i<br>>
Specify of the details of this request. This request will be referred to as the 'Pre Login Request' in subsequent sections.
<i<br>><i<br>>
")},
        {HandleLogin__LoginRequestParametersStep, @"<i<hh>>'Login Request' Parameter Update Definition<i</hh>>
<i<br>><i<br>>
The parameters of the 'Login Request' will be updated based on your requirements.
<i<br>><i<br>>
You must first specify the section of the request the parameter belongs to like Query, Body etc and  the name of the parameter. This way the parameter to update can be identified.
<i<br>><i<br>>
If you want to update the path section of the URL incase the server is using URL Rewriting then select 'UrlPathPart' and give the zero-based index of the path section you want to update.
<i<br>><i<br>>
Then the value to update this parameter with must be made ready. If the value is being extracted from a response of the 'Pre Login Request' request then you must specify how this value can be extracted.
<i<br>><i<br>>
If this value occurs inside the value attribute of input tags in the response then it is taken from the first input tag that has the same name as the parameter name provided earlier.
<i<br>><i<br>>
If this value occurs in a different place then you can provide a regex to extract this value. This regex will be used on the entire response body and the first match will be used.
<i<br>><i<br>>
For example if the token appears inside the SCRIPT tag of the response as below:
<i<br>><i<br>>
var c_token = 'ksd9283-asdjsd023-askjd23';
<i<br>><i<br>>
Then the regex to extract this token would be: var c_token = '(.*?)';
<i<br>><i<br>>
If you would want to get the value of this parameter from the user by throwing a prompt then you can select that option and provide a hint that will be included in the prompt to help the user identify the correct parameter.
<i<br>><i<br>>
You can choose to update multiple parameters in this section. After you are done defining the parameters and the parameters are listed in the table at the bottom you can submit this definition.
<i<br>><i<br>>
"},
 //"Choose how the parameters of the 'Login Request' must be updated from the response of the 'Pre Login Request'."},
        {HandleLogin__ToInjectRequestParametersStep, @"<i<hh>>Update New Session values after Successful Login<i</hh>>
<i<br>><i<br>>
Once the 'Login Request' is sent and the authentication happens in the server the new session values must be updated in to the 'Main Request'.
<i<br>><i<br>>
If the session identifier is stored in the cookie and the response of the 'Login Request' contained the new session identifier in its Set-Cookie header then this is automatically updated in the 'Main Request' and no action is required from your side.
<i<br>><i<br>>
However if this application stores session identifiers in hidden form field parameters or in some other places then you would have to extract it from the response of the 'Login Request' and update this in to the 'Main Request'.
<i<br>><i<br>>
You can define how this should be done.
<i<br>><i<br>>
You must first specify the section of the request the parameter belongs to like Query, Body etc and  the name of the parameter. This way the parameter to update can be identified.
<i<br>><i<br>>
If you want to update the path section of the URL incase the server is using URL Rewriting then select 'UrlPathPart' and give the zero-based index of the path section you want to update.
<i<br>><i<br>>
Then the value to update this parameter with must be made ready. If the value is being extracted from a response of the 'Login Request' request then you must specify how this value can be extracted.
<i<br>><i<br>>
If this value occurs inside the value attribute of input tags in the response then it is taken from the first input tag that has the same name as the parameter name provided earlier.
<i<br>><i<br>>
If this value occurs in a different place then you can provide a regex to extract this value. This regex will be used on the entire response body and the first match will be used.
<i<br>><i<br>>
For example if the token appears inside the SCRIPT tag of the response as below:
<i<br>><i<br>>
var c_token = 'ksd9283-asdjsd023-askjd23';
<i<br>><i<br>>
Then the regex to extract this token would be: var c_token = '(.*?)';
<i<br>><i<br>>
If you would want to get the value of this parameter from the user by throwing a prompt then you can select that option and provide a hint that will be included in the prompt to help the user identify the correct parameter.
<i<br>><i<br>>
You can choose to update multiple parameters in this section. After you are done defining the parameters and the parameters are listed in the table at the bottom you can submit this definition.
<i<br>><i<br>>
"},
 //"Once the 'Login Request' is sent and we get authenticated, we must update the 'Main Request' with updated session values. Choose how the parameters of the 'Main Request' must be updated from the response of the 'Login Request'. Please notes that cookies are updated automatically from set-cookie headers in this step."},
        {HandleLogin__ShowPseudoCodeStep, string.Format(GenericQuestionToShowPseudoCode, @"<i<hh>>Pseudo Code for Handle Login Action<i</hh>>
<i<br>><i<br>>")},
        //MutliStep Form
        {HandleMultiStep__BaseStep,  @"<i<hh>>Handle Multi-step Form Submission<i</hh>>
<i<br>><i<br>>
Sometimes applications have multi-step forms where there would user will be asked to complete a series of steps and at the final step all entered data will be processed.
<i<br>><i<br>>
If you are scanning or testing a request that belongs any one of these steps then the requests that come before and after it must be sent everytime and the response of the final step must be used for analysis instead of the response of the 'Main Request'.
<i<br>><i<br>>
If such behaviour is required then you can define it here by selecting 'Yes' below and hitting the 'Submit' button.
<i<br>><i<br>>
If you have already defined this action and have the corresponding Pseudo Code and you like to use the same then selected the 'Pseudo Code' option.
<i<br>><i<br>>
"},
        {HandleMultiStep__PseudoCodeStep, @"<i<hh>>Handle Multi-step Form Submission Pseudo Code<i</hh>>
<i<br>><i<br>>
If you have already defined the multi-step form submission action then enter the corresponding pseudo code below.
<i<br>><i<br>>
"},
        {HandleMultiStep__BeforeAfterInjectionSelectionStep, @"<i<hh>>Multi-step Form Submission Definition<i</hh>>
<i<br>><i<br>>
If you have chosen to define that actions for multi-step form submission.
<i<br>><i<br>>
Specify if would want to do other steps of the form submission only before sending the 'Main Request' or only after sending the 'Main Request' or both before and after sending the 'Main Request'.
<i<br>><i<br>>
For example if the 'Main Request' is the first step of multi-step form submission then you would want to select the second option where the other steps are performed only after the 'Main Request'.
<i<br>><i<br>>
If the 'Main Request' is the last step then you would want to select the first option where the other steps are performed only before the 'Main Request'
<i<br>><i<br>>
If the 'Main Request' is one of the intermediate steps then you would want to select the third option where some steps must be performed before the 'Main Request' and some after.
<i<br>><i<br>>
"},
        {HandleMultiStep__PreInjectionRequestSourceStep, string.Format(GenericQuestionToSpecifyRequestSource, @"<i<hh>>{0} Request to send before 'Main Request'<i</hh>>
<i<br>><i<br>>
You have chose to sends some requests before sending the 'Main Request', please specify the {0} request that must be sent.
<i<br>><i<br>>
Provide an approprite name for this request, it will be referred using this name in the subsequent sections and the Session Plugin trace messages.
<i<br>><i<br>>
")},
//@"Specify the source of the {0} request that must be sent before injection."},
        {HandleMultiStep__PreInjectionRequestParametersStep, @"<i<hh>>'{0}' Parameter Update Definition<i</hh>>
<i<br>><i<br>>
The parameters of the '{0}' will be updated based on your requirements.
<i<br>><i<br>>
You must first specify the section of the request the parameter belongs to like Query, Body etc and  the name of the parameter. This way the parameter to update can be identified.
<i<br>><i<br>>
If you want to update the path section of the URL incase the server is using URL Rewriting then select 'UrlPathPart' and give the zero-based index of the path section you want to update.
<i<br>><i<br>>
Then the value to update this parameter with must be made ready. If the value is being extracted from a response of the '{1}' request then you must specify how this value can be extracted.
<i<br>><i<br>>
If this value occurs inside the value attribute of input tags in the response then it is taken from the first input tag that has the same name as the parameter name provided earlier.
<i<br>><i<br>>
If this value occurs in a different place then you can provide a regex to extract this value. This regex will be used on the entire response body and the first match will be used.
<i<br>><i<br>>
For example if the token appears inside the SCRIPT tag of the response as below:
<i<br>><i<br>>
var c_token = 'ksd9283-asdjsd023-askjd23';
<i<br>><i<br>>
Then the regex to extract this token would be: var c_token = '(.*?)';
<i<br>><i<br>>
If you would want to get the value of this parameter from the user by throwing a prompt then you can select that option and provide a hint that will be included in the prompt to help the user identify the correct parameter.
<i<br>><i<br>>
You can choose to update multiple parameters in this section. After you are done defining the parameters and the parameters are listed in the table at the bottom you can submit this definition.
<i<br>><i<br>>
If you don't wish to update any parameters just hit 'Submit'
<i<br>><i<br>>
"},
        {HandleMultiStep__MorePreInjectionRequestStep, @"<i<hh>>More Requests before 'Main Request'?<i</hh>>
<i<br>><i<br>>
Should more requests be sent before sending the 'Main Request'?
<i<br>><i<br>>
"},
        {HandleMultiStep__PostInjectionRequestSourceStep, string.Format(GenericQuestionToSpecifyRequestSource, @"<i<hh>>{0} Request to send after 'Main Request'<i</hh>>
<i<br>><i<br>>
You have chose to sends some requests after sending the 'Main Request', please specify the {0} request that must be sent.
<i<br>><i<br>>
Provide an approprite name for this request, it will be referred using this name in the subsequent sections and the Session Plugin trace messages.
<i<br>><i<br>>
")},
//@"Specify the source of the {0} request that must be sent after injection."},
        {HandleMultiStep__PostInjectionRequestParametersStep, @"<i<hh>>'{0}' Parameter Update Definition<i</hh>>
<i<br>><i<br>>
The parameters of the '{0}' will be updated based on your requirements.
<i<br>><i<br>>
You must first specify the section of the request the parameter belongs to like Query, Body etc and  the name of the parameter. This way the parameter to update can be identified.
<i<br>><i<br>>
If you want to update the path section of the URL incase the server is using URL Rewriting then select 'UrlPathPart' and give the zero-based index of the path section you want to update.
<i<br>><i<br>>
Then the value to update this parameter with must be made ready. If the value is being extracted from a response of the '{1}' request then you must specify how this value can be extracted.
<i<br>><i<br>>
If this value occurs inside the value attribute of input tags in the response then it is taken from the first input tag that has the same name as the parameter name provided earlier.
<i<br>><i<br>>
If this value occurs in a different place then you can provide a regex to extract this value. This regex will be used on the entire response body and the first match will be used.
<i<br>><i<br>>
For example if the token appears inside the SCRIPT tag of the response as below:
<i<br>><i<br>>
var c_token = 'ksd9283-asdjsd023-askjd23';
<i<br>><i<br>>
Then the regex to extract this token would be: var c_token = '(.*?)';
<i<br>><i<br>>
If you would want to get the value of this parameter from the user by throwing a prompt then you can select that option and provide a hint that will be included in the prompt to help the user identify the correct parameter.
<i<br>><i<br>>
You can choose to update multiple parameters in this section. After you are done defining the parameters and the parameters are listed in the table at the bottom you can submit this definition.
<i<br>><i<br>>
If you don't wish to update any parameters just hit 'Submit'
<i<br>><i<br>>
"},
        {HandleMultiStep__MorePostInjectionRequestStep, @"<i<hh>>More Requests after 'Main Request'?<i</hh>>
<i<br>><i<br>>
Should more requests be sent after sending the 'Main Request'?
<i<br>><i<br>>
"},
        {HandleMultiStep__ShowPseudoCodeStep, string.Format(GenericQuestionToShowPseudoCode, @"<i<hh>>Pseudo Code for Handle Multi-step Form Submission Action<i</hh>>
<i<br>><i<br>>")},
        //PluginCreation Form
        {PluginCreation__PluginNameStep, @"<i<hh>>Choose a name for the Session Plugin<i</hh>>
<i<br>><i<br>>
A Session Plugin will be created based on the actions you have defined. Please provide a name for this plugin.
<i<br>><i<br>>
The name for the plugin should only consist of alphabets and should not match any of the existing Session Plugin names. After you type in the name of your choice, press the Enter key on your keyboard.
<i<br>><i<br>>
The following are the existing Session Plugin names:
<i<br>><i<br>>
{0}
<i<br>><i<br>>
        "},
        {PluginCreation__LanguageStep, @"<i<hh>>Choose the language for the Session Plugin<i</hh>>
<i<br>><i<br>>
The Session Plugin can be created in Python or Ruby. You could choose one based on which language is more familiar to you. This will help you review the plugin if you wish to and make modifications to it directly.
<i<br>><i<br>>
If you don't know what Python/Ruby mean or you don't know programming then you can just randonmly select one of the two choices, they both provide the same functionality.
<i<br>><i<br>>
"},
        {PluginCreation__FinalStep, @"<i<hh>>Session Plugin Creation Complete<i</hh>>
<i<br>><i<br>>
The Session Plugin can be created based on the inputs provided by you.
<i<br>><i<br>>
You can now use this in sections of IronWASP that support Session Plugins. Just select the one with the name provided by you when you are prompted to select a Session Plugin.
<i<br>><i<br>>
As the Session Plugin runs and executes the actions you defined it prints out trace messages so that you can follow how and if it is working properly.
<i<br>><i<br>>
These messages can be found at 'Dev' -> 'Trace' -> 'Session Plugin Traces'.
<i<br>><i<br>>
You can click on any of the entries to view details.
<i<br>><i<br>>
The file containing the code itself can be found at {0}
<i<br>><i<br>>
You can view it using your favourite ide or use the Script/Plugin editor available under the Dev Tools menu. If you make any changes to the code then save the file, go the 'Dev' -> 'Plugins & Modules' section, you can see all the Plugin names listed on the left-hand side. Click on the name of your Session Plugin, once its selected do a right-click on it and select 'Reload Selected Module'. This will update the plugin in memory so that you changes take effect.
"},

};


        static Dictionary<string, List<string>> QuestionOptions = new Dictionary<string, List<string>>() {
        //Home Menu
        {HomeMenu__BaseStep, new List<string>(){"Read Set-Cookie headers","Update CSRF tokens and/or other parameters in request", "Handle Login", 
            "Follow Redirects", "Handle Multi-step Form Submission"}},
        
        //Handle Redirect
        {HandleRedirect__BaseStep, new List<string>(){"Yes, define handle redirect action", "No, go back to main menu", "I have Pseudo Code for this action"}},

        // Handle SetCookies
        {HandleSetCookies__BaseStep, new List<string>(){"Yes, define take the Set-Cookie parameter names", "No, go back to main menu"}},

        //Handle CSRF Tokens
        {HandleCSRFTokens__BaseStep, new List<string>(){"Yes, define csrf-totken update action", "No, go back to main menu", "I have Pseudo Code for this action"}},

        {HandleCSRFTokens__ParameterSourceStep, new List<string>(){"Send another request and get values from its response", "Ask the user everytime"}},

        //Handle Login
        {HandleLogin__BaseStep, new List<string>(){"Yes, define handle login action", "No, go back to main menu", "I have Pseudo Code for this action"}},

        {HandleLogin__ShouldUpdateLoginRequestStep, new List<string>(){"Yes", "No"}},
        
        {HandleLogin__IsPreLoginRequestNeededStep, new List<string>(){"Yes", "No"}},

        //MutliStep Form
        {HandleMultiStep__BaseStep, new List<string>(){"Yes", "No, go back to main menu", "I have Pseudo Code for this action"}},
        
        {HandleMultiStep__BeforeAfterInjectionSelectionStep, new List<string>(){"Before 'Main Request' only", "After 'Main Request' only", "Both before and after"}},

        {HandleMultiStep__MorePreInjectionRequestStep, new List<string>(){"Yes", "No" }},

        {HandleMultiStep__MorePostInjectionRequestStep, new List<string>(){"Yes", "No"}},

        //PluginCreation Form
        {PluginCreation__LanguageStep, new List<string>(){"Python", "Ruby"}},
        };

        #endregion



        string CurrentSection = HomeMenu;
        
        string CurrentStep = "-";

        public SessionPluginCreationAssistant()
        {
            InitializeComponent();
        }

        void ShowError(string Error)
        {
            switch(CurrentQuestionType)
            {
                case(QuestionTypeTextAnswer):
                    StatusTB.Text = Error;
                    StatusTB.ForeColor = Color.Red;
                    StatusTB.Visible = true;
                    break;
                case (QuestionTypeRequestSourceAnswer):
                    RequestSourceAnswerMsgTB.Text = Error;
                    RequestSourceAnswerMsgTB.ForeColor = Color.Red;
                    RequestSourceAnswerMsgTB.Visible = true;
                    break;
                case (QuestionTypeParametersAnswer):
                    ParametersAnswerMsgTB.Text = Error;
                    ParametersAnswerMsgTB.ForeColor = Color.Red;
                    ParametersAnswerMsgTB.Visible = true;
                    break;
                case (QuestionTypeResponseSignatureAnswer):
                    SignatureAnswerMsgTB.Text = Error;
                    SignatureAnswerMsgTB.ForeColor = Color.Red;
                    SignatureAnswerMsgTB.Visible = true;
                    break;
                case(QuestionTypeSelectOptionAnswer):
                    SelectOptionAnswerMsgTB.Text = Error;
                    SelectOptionAnswerMsgTB.ForeColor = Color.Red;
                    SelectOptionAnswerMsgTB.Visible = true;
                    break;
            }
        }
        //void ShowStatus(string Status)
        //{
        //    StatusTB.Text = Status;
        //    StatusTB.ForeColor = Color.Black;
        //    StatusTB.Visible = true;
        //}
        void ClearStatus()
        {
            StatusTB.Visible = false;
            StatusTB.Text = "";
        }

        #region ShowQuestions
        void ShowQuestion(string Question, bool ExpectOneLineInput)
        {
            ClearStatus();
            SetQuestionText(Question);
            
            BigAnswerTB.Text = "";
            AnswerTB.Text = "";

            if (ExpectOneLineInput)
            {
                AnswerTB.Enabled = true;
                BigAnswerTB.Enabled = false;
                AnswerTB.Focus();
            }
            else
            {
                BigAnswerTB.Enabled = true;
                AnswerTB.Enabled = false;
                BigAnswerTB.Focus();
            }
            CurrentQuestionType = QuestionTypeTextAnswer;
            AnswerTabs.SelectTab("TextAnswerTab");
        }

        void ShowRequestSourceQuestion(string Question, string RequestName)
        {
            SetQuestionText(Question);
            RequestSourceAnswerMsgTB.Text = "";
            RequestSourceCombo.Text = "";
            RequestSourceCombo.SelectedIndex = 0;
            RequestSourceIdTB.Text = "";
            if (RequestName.Length > 0)
            {
                RequestSourceNameTB.Text = RequestName;
                RequestSourceNameTB.Enabled = false;
            }
            else
            {
                RequestSourceNameTB.Text = "";
                RequestSourceNameTB.Enabled = true;
            }
            CurrentQuestionType = QuestionTypeRequestSourceAnswer;
            AnswerTabs.SelectTab("RequestSourceAnswerTab");
        }

        void ShowResponseSignatureQuestion(string Question, bool FullResponse)
        {
            SetQuestionText(Question);
            SignatureAnswerMsgTB.Text = "";

            SignatureResponseCodeTB.Text = "";

            UseLocationSignatureCB.Checked = false;
            LocationSignatureTypeSelectCombo.SelectedIndex = 2;
            LocationSignatureKeywordTB.Text = "";
            LocationSignatureKeywordTB.Enabled = false;

            LoggedInResponseSignatureRB.Checked = false;
            LoggedOutResponseSignatureRB.Checked = false;

            UseTitleSignatureCB.Checked = false;
            TitleSignatureTypeSelectCombo.SelectedIndex = 2;
            TitleSignatureKeywordTB.Text = "";

            UseBodySignatureCB.Checked = false;
            BodySignatureTypeSelectCombo.SelectedIndex = 2;
            BodySignatureKeywordTB.Text = "";

            LocationSignatureTypeSelectCombo.Enabled = false;
            if (FullResponse)
            {
                FullResponseSignatureFirstPanel.Visible = true;
                FullResponseSignatureSecondPanel.Visible = true;

                TitleSignatureTypeSelectCombo.SelectedIndex = 2;
                TitleSignatureTypeSelectCombo.Enabled = false;
                TitleSignatureKeywordTB.Enabled = false;

                BodySignatureTypeSelectCombo.SelectedIndex = 2;
                BodySignatureTypeSelectCombo.Enabled = false;
                BodySignatureKeywordTB.Enabled = false;
            }
            else
            {
                FullResponseSignatureFirstPanel.Visible = false;
                FullResponseSignatureSecondPanel.Visible = false;
            }
            ResponseSignatureFullResponse = FullResponse;
            CurrentQuestionType = QuestionTypeResponseSignatureAnswer;
            AnswerTabs.SelectTab("ResponseSignatureTab");
        }

        void ShowParameterUpdateQuestion(string Question, bool AskUserOnly)
        {
            SetQuestionText(Question);
            ResetParameterAnswerFields(true);

            ParametersAnswerAskUserOnly = AskUserOnly;
            CurrentQuestionType = QuestionTypeParametersAnswer;
            AnswerTabs.SelectTab("ParameterAnswerTab");
        }

        void ShowSelectOptionQuestion(string Question, List<string> Options, string SpecialOptionText)
        {
            SetQuestionText(Question);
            ResetParameterAnswerFields(true);

            OptionsGrid.Rows.Clear();
            foreach (string Option in Options)
            {
                OptionsGrid.Rows.Add(new object[]{false, Option});
            }
            
            if (OptionsGrid.Rows.Count > 0)
                SelectedOptionSubmitBtn.Enabled = true;
            else
                SelectedOptionSubmitBtn.Enabled = false;

            SpecialOptionBtn.Text = SpecialOptionText;
            
            if (SpecialOptionText.Length > 0)
                SpecialOptionBtn.Visible = true;
            else
                SpecialOptionBtn.Visible = false;


            CurrentQuestionType = QuestionTypeSelectOptionAnswer;
            AnswerTabs.SelectTab("SelectOptionTab");
        }

        void ShowPreInjectionRequestSourceQuestion()
        {
            CurrentStep = HandleMultiStep__PreInjectionRequestSourceStep;
            PreInjectionCounter++;
            if (PreInjectionCounter == 1)
                MultiStepActionPseudoCode = string.Format("PreInjectionCode:{0}", Environment.NewLine);
            string Q = string.Format(Questions[HandleMultiStep__PreInjectionRequestSourceStep], GetNumberWithSuffix(PreInjectionCounter));
            ShowRequestSourceQuestion(Q, "");
        }

        void ShowPostInjectionRequestSourceQuestion()
        {
            CurrentStep = HandleMultiStep__PostInjectionRequestSourceStep;
            PostInjectionCounter++;
            if (PostInjectionCounter == 1)
            {
                StringBuilder SB = new StringBuilder();
                SB.Append(MultiStepActionPseudoCode);
                SB.AppendLine("PostInjectionCode:");
                MultiStepActionPseudoCode = SB.ToString();
            }
            string Q = string.Format(Questions[HandleMultiStep__PostInjectionRequestSourceStep], GetNumberWithSuffix(PostInjectionCounter));
            ShowRequestSourceQuestion(Q, "");
        }

        void ShowPreInjectionParametersUpdateQuestions()
        {
            CurrentStep = HandleMultiStep__PreInjectionRequestParametersStep;
            string Q = "";
            if (PreInjectionCounter == PreInjectionCounterMax)
                //Q = string.Format(Questions[HandleMultiStep__PreInjectionRequestParametersStep], "'ToInject'", GetNumberWithSuffix(PreInjectionCounter));
                Q = string.Format(Questions[HandleMultiStep__PreInjectionRequestParametersStep], "'Main Request'", this.RequestNamesFromUserList[this.RequestNamesFromUserList.Count - 1]);
            else
                //Q = string.Format(Questions[HandleMultiStep__PreInjectionRequestParametersStep], GetNumberWithSuffix(PreInjectionCounter), GetNumberWithSuffix(PreInjectionCounter - 1));
                Q = string.Format(Questions[HandleMultiStep__PreInjectionRequestParametersStep], this.RequestNamesFromUserList[this.RequestNamesFromUserList.Count - 1], this.RequestNamesFromUserList[this.RequestNamesFromUserList.Count - 2]);
            ShowParameterUpdateQuestion(Q, false);
        }

        void ShowPostInjectionParametersUpdateQuestions()
        {
            CurrentStep = HandleMultiStep__PostInjectionRequestParametersStep;
            string Q = "";
            if (PostInjectionCounter == 1)
                //Q = string.Format(Questions[HandleMultiStep__PostInjectionRequestParametersStep], GetNumberWithSuffix(PostInjectionCounter), "'ToInject'");
                Q = string.Format(Questions[HandleMultiStep__PostInjectionRequestParametersStep], this.RequestNamesFromUserList[this.RequestNamesFromUserList.Count - 1], "'Main Request'");
            else
                //Q = string.Format(Questions[HandleMultiStep__PostInjectionRequestParametersStep], GetNumberWithSuffix(PostInjectionCounter), GetNumberWithSuffix(PostInjectionCounter -1));
                Q = string.Format(Questions[HandleMultiStep__PostInjectionRequestParametersStep], this.RequestNamesFromUserList[this.RequestNamesFromUserList.Count - 1], this.RequestNamesFromUserList[this.RequestNamesFromUserList.Count - 2]);
            ShowParameterUpdateQuestion(Q, false);
        }

        void ShowPseudoCode(string Question, string PseudoCode)
        {
            SetQuestionText(Question);
            ShowPseudoCodeTB.Text = PseudoCode;
            CurrentQuestionType = QuestionTypeShowPseudoCode;
            AnswerTabs.SelectTab("ShowPseudoCodeTab");
        }

        void SetQuestionText(string Question)
        {
            StringBuilder SB = new StringBuilder(@"{\rtf1{\colortbl ;\red0\green77\blue187;\red247\green150\blue70;\red255\green0\blue0;\red0\green200\blue50;}");
            SB.Append(Tools.RtfSafe(Question));
            SB.Append(@" \par");          
            QuestionRTB.Rtf = SB.ToString();
        }


        #endregion


        #region ProcessAnswer
        void ProcessUserAnswer()
        {
            switch(CurrentSection)
            {
                case(HomeMenu):
                    ProcessHomeMenuAnswer();
                    break;
                case (HandleRedirect):
                    ProcessHandleRedirectAnswer();
                    break;
                case (HandleCSRFTokens):
                    ProcessHandleCSRFTokensAnswer();
                    break;
                //case (HandleEncodedParameters):
                //    ProcessHandleEncodedParametersAnswer();
                //    break;
                case (HandleLogin):
                    ProcessHandleLoginAnswer();
                    break;
                case (HandleMultiStep):
                    ProcessHandleMultiStepAnswer();
                    break;
                case (HandleSetCookies):
                    ProcessHandleSetCookiesAnswer();
                    break;
                case(PluginCreation):
                    ProcessPluginCreationAnswer();
                    break;
            }
        }

        void ProcessHomeMenuAnswer()
        {
            string Answer = AnswerTB.Text.Trim();
            switch (Answer)
            {
                case ("0"):
                    if (NamesOfCookieParametersToUpdate.Count > 0 || UpdateCSRFTokensPseudoCode.Length > 0 || LoginActionPseudoCode.Length > 0 || FollowRedirectPseudoCode.Length > 0 || MultiStepActionPseudoCode.Length > 0)
                    {
                        CurrentSection = PluginCreation;
                        CurrentStep = PluginCreation__PluginNameStep;
                        ShowQuestion(string.Format(Questions[PluginCreation__PluginNameStep], string.Join("<i<br>>", SessionPlugin.List().ToArray())), true);
                    }
                    else
                    {
                        ShowError("You must first define 1 or more of the 5 actions before creating the Session Plugin");
                    }
                    break;
                case ("1"):
                    CurrentSection = HandleSetCookies;
                    CurrentStep = HandleSetCookies__BaseStep;
                    this.RequestNamesFromUserList.Clear();
                    //ShowQuestion(Questions[HandleSetCookies__BaseStep], true);
                    ShowSelectOptionQuestion(Questions[HandleSetCookies__BaseStep], QuestionOptions[HandleSetCookies__BaseStep], "");
                    break;
                case("2"):
                    CurrentSection = HandleCSRFTokens;
                    CurrentStep = HandleCSRFTokens__BaseStep;
                    this.RequestNamesFromUserList.Clear();
                    //ShowQuestion(Questions[HandleCSRFTokens__BaseStep], true);
                    ShowSelectOptionQuestion(Questions[HandleCSRFTokens__BaseStep], QuestionOptions[HandleCSRFTokens__BaseStep], "");
                    break;
                case ("3"):
                    CurrentSection = HandleLogin;
                    CurrentStep = HandleLogin__BaseStep;
                    this.RequestNamesFromUserList.Clear();
                    //ShowQuestion(Questions[HandleLogin__BaseStep], true);
                    ShowSelectOptionQuestion(Questions[HandleLogin__BaseStep], QuestionOptions[HandleLogin__BaseStep], "");
                    break;
                case ("4"):
                    CurrentSection = HandleRedirect;
                    CurrentStep = HandleRedirect__BaseStep;
                    this.RequestNamesFromUserList.Clear();
                    //ShowQuestion(Questions[HandleRedirect__BaseStep], true);
                    ShowSelectOptionQuestion(Questions[HandleRedirect__BaseStep], QuestionOptions[HandleRedirect__BaseStep], "");
                    break;
                case ("5"):
                    CurrentSection = HandleMultiStep;
                    CurrentStep = HandleMultiStep__BaseStep;
                    this.RequestNamesFromUserList.Clear();
                    //ShowQuestion(Questions[HandleMultiStep__BaseStep], true);
                    ShowSelectOptionQuestion(Questions[HandleMultiStep__BaseStep], QuestionOptions[HandleMultiStep__BaseStep], "");
                    break;
                //case ("6"):
                //    CurrentSection = HandleEncodedParameters;
                //    break;
                default:
                    ShowError("Invalid option, try again");
                    break;
            }
        }

        void ProcessHandleRedirectAnswer()
        {
            string Answer = "";
            switch(CurrentStep)
            {
                case(HandleRedirect__BaseStep):
                    Answer = AnswerTB.Text.Trim();
                    switch (Answer)
                    {
                        case("1"):
                            CurrentStep = HandleRedirect__ResponseSignatureStep;
                            ShowResponseSignatureQuestion(Questions[HandleRedirect__ResponseSignatureStep], false);
                            break;
                        case ("2"):
                            GoToHomeMenu();
                            break;
                        case ("3"):
                            CurrentStep = HandleRedirect__EnterPseudoCodeStep;
                            ShowQuestion(Questions[HandleRedirect__EnterPseudoCodeStep], true);
                            break;
                        default:
                            ShowError("Invalid option, try again");
                            break;
                    }
                    break;
                case(HandleRedirect__ResponseSignatureStep):
                    try
                    {
                        FollowRedirectPseudoCode = RedirectSignatureToPseudoCode();
                        string[] Code = FollowRediretPseudoCodeToCode();
                        CurrentStep = HandleRedirect__ShowPseudoCodeStep;
                        //ShowQuestion(string.Format(Questions[HandleRedirect__ShowPseudoCodeStep], FollowRedirectPseudoCode), true);
                        //ShowSelectOptionQuestion(string.Format(Questions[HandleRedirect__ShowPseudoCodeStep], FollowRedirectPseudoCode), new List<string>() { }, "Go to Home Menu");
                        ShowPseudoCode(Questions[HandleRedirect__ShowPseudoCodeStep], FollowRedirectPseudoCode);
                    }
                    catch { ShowError("Invalid input, try again"); }
                    break;
                case (HandleRedirect__EnterPseudoCodeStep):
                    Answer = AnswerTB.Text.Trim();
                    FollowRedirectPseudoCode = Answer;
                    GoToHomeMenu();
                    break;
                case(HandleRedirect__ShowPseudoCodeStep):
                    GoToHomeMenu();
                    break;
            }
        }

        void ProcessHandleCSRFTokensAnswer()
        {
            string Answer = AnswerTB.Text.Trim();
            switch(CurrentStep)
            {
                case(HandleCSRFTokens__BaseStep):
                    switch (Answer)
                    {
                        case("1"):
                            UpdateCSRFTokensPseudoCode = "";
                            CurrentStep = HandleCSRFTokens__ParameterSourceStep;
                            //ShowQuestion(Questions[HandleCSRFTokens__ParameterSourceStep], true);
                            ShowSelectOptionQuestion(Questions[HandleCSRFTokens__ParameterSourceStep], QuestionOptions[HandleCSRFTokens__ParameterSourceStep], "");
                            break;
                        case ("2"):
                            GoToHomeMenu();
                            break;
                        case ("3"):
                            CurrentStep = HandleCSRFTokens__PseudoCodeStep;
                            ShowQuestion(Questions[HandleCSRFTokens__PseudoCodeStep], false);
                            break;
                        default:
                            ShowError("Invalid option selected, try again.");
                            break;
                    }
                    break;
                case(HandleCSRFTokens__ParameterSourceStep):
                    switch (Answer)
                    {
                        case("1"):
                            CurrentStep = HandleCSRFTokens__RequestToSendForResponseStep;
                            ShowRequestSourceQuestion(Questions[HandleCSRFTokens__RequestToSendForResponseStep], "");
                            break;
                        case ("2"):
                            CurrentStep = HandleCSRFTokens__ParametersStep;
                            //ShowParameterUpdateQuestion(Questions[HandleCSRFTokens__ParametersStep], true);
                            ShowParameterUpdateQuestion(string.Format(Questions[HandleCSRFTokens__ParametersAskUserOnlyStep], "Main Request"), true);
                            break;
                        default:
                            ShowError("Invalid option selected, try again.");
                            break;
                    }
                    break;
                case(HandleCSRFTokens__RequestToSendForResponseStep):
                    try
                    {
                        UpdateCSRFTokensPseudoCode = RequestSourceToPseudoCode();
                        StringBuilder SB = new StringBuilder(UpdateCSRFTokensPseudoCode);
                        SB.AppendLine("SendRequest");
                        SB.AppendLine("GetRequest ToInject");
                        UpdateCSRFTokensPseudoCode = SB.ToString();
                        CurrentStep = HandleCSRFTokens__ParametersStep;
                        if(this.RequestNamesFromUserList.Count > 0)
                            ShowParameterUpdateQuestion(string.Format(Questions[HandleCSRFTokens__ParametersStep], "Main Request", this.RequestNamesFromUserList[this.RequestNamesFromUserList.Count - 1]), false);
                        else
                            ShowParameterUpdateQuestion(string.Format(Questions[HandleCSRFTokens__ParametersStep], "Main Request", "previous unnamed request"), false);
                    }
                    catch { ShowError("Invalid answer, try again."); }
                    break;
                case(HandleCSRFTokens__ParametersStep):
                    StringBuilder PC = new StringBuilder(UpdateCSRFTokensPseudoCode);
                    PC.Append(ParametersAnswerToPseudoCode());
                    UpdateCSRFTokensPseudoCode = PC.ToString();
                    CurrentStep = HandleCSRFTokens__ShowPseudoCodeStep;
                    //ShowQuestion(string.Format(Questions[HandleCSRFTokens__ShowPseudoCodeStep], UpdateCSRFTokensPseudoCode), true);
                    //ShowSelectOptionQuestion(string.Format(Questions[HandleCSRFTokens__ShowPseudoCodeStep], UpdateCSRFTokensPseudoCode), new List<string>() { }, "Go to Home Menu");
                    ShowPseudoCode(Questions[HandleCSRFTokens__ShowPseudoCodeStep], UpdateCSRFTokensPseudoCode);
                    break;
                case (HandleCSRFTokens__PseudoCodeStep):
                    Answer = BigAnswerTB.Text.Trim();
                    UpdateCSRFTokensPseudoCode = Answer;
                    GoToHomeMenu();
                    break;
                case(HandleCSRFTokens__ShowPseudoCodeStep):
                    GoToHomeMenu();
                    break;
            }
        }

        //void ProcessHandleEncodedParametersAnswer()
        //{
            
        //}

        void ProcessHandleLoginAnswer()
        {
            string Answer = AnswerTB.Text.Trim();
            switch (CurrentStep)
            {
                case (HandleLogin__BaseStep):
                    switch (Answer)
                    {
                        case ("1"):
                            CurrentStep = HandleLogin__LoggedinCheckRequestSourceStep;
                            ShowRequestSourceQuestion(Questions[HandleLogin__LoggedinCheckRequestSourceStep], "Login Check");
                            break;
                        case ("2"):
                            GoToHomeMenu();
                            break;
                        case ("3"):
                            CurrentStep = HandleLogin__PseudoCodeStep;
                            ShowQuestion(Questions[HandleLogin__PseudoCodeStep], false);
                            break;
                        default:
                            ShowError("Invalid option entered");
                            break;
                    }
                    break;
                case (HandleLogin__PseudoCodeStep):
                    LoginActionPseudoCode = BigAnswerTB.Text.Trim();
                    GoToHomeMenu();
                    break;
                case (HandleLogin__LoggedinCheckRequestSourceStep):
                    try
                    {
                        LoginActionPseudoCode = RequestSourceToPseudoCode();
                        StringBuilder SB = new StringBuilder();
                        SB.Append(LoginActionPseudoCode);
                        SB.AppendLine("SendRequest");
                        LoginActionPseudoCode = SB.ToString();
                        CurrentStep = HandleLogin__LoggedInLoggedOutSignatureResponseSectionStep;
                        ShowResponseSignatureQuestion(Questions[HandleLogin__LoggedInLoggedOutSignatureResponseSectionStep], true);
                    }
                    catch { ShowError("Invalid Input, try again."); }
                    break;
                case (HandleLogin__LoggedInLoggedOutSignatureResponseSectionStep):
                    try
                    {
                        string LoginLoggedSignaturePseudoCode = ResponseSignatureToPseudoCode();
                        StringBuilder SB = new StringBuilder();
                        SB.Append(LoginActionPseudoCode);
                        SB.Append(LoginLoggedSignaturePseudoCode);
                        LoginActionPseudoCode = SB.ToString();
                        CurrentStep = HandleLogin__LoginRequestSourceStep;
                        ShowRequestSourceQuestion(Questions[HandleLogin__LoginRequestSourceStep], "Login");
                    }
                    catch { ShowError("Invalid Input, try again."); }
                    break;
                case (HandleLogin__LoginRequestSourceStep):
                    try
                    {
                        LoginRequestSourcePseudoCode = RequestSourceToPseudoCode();
                        CurrentStep = HandleLogin__ShouldUpdateLoginRequestStep;
                        //ShowQuestion(Questions[HandleLogin__ShouldUpdateLoginRequestStep], true);
                        ShowSelectOptionQuestion(Questions[HandleLogin__ShouldUpdateLoginRequestStep], QuestionOptions[HandleLogin__ShouldUpdateLoginRequestStep], "");
                    }
                    catch { ShowError("Invalid Input, try again."); }
                    break;
                case (HandleLogin__ShouldUpdateLoginRequestStep):
                    try
                    {
                        Answer = AnswerTB.Text.Trim();
                        switch (Answer)
                        {
                            case("1"):
                                CurrentStep = HandleLogin__IsPreLoginRequestNeededStep;
                                //ShowQuestion(Questions[HandleLogin__IsPreLoginRequestNeededStep], true);
                                ShowSelectOptionQuestion(Questions[HandleLogin__IsPreLoginRequestNeededStep], QuestionOptions[HandleLogin__IsPreLoginRequestNeededStep], "");
                                break;
                            case ("2"):
                                StringBuilder SB = new StringBuilder();
                                SB.Append(LoginActionPseudoCode);
                                SB.Append(LoginRequestSourcePseudoCode);
                                LoginActionPseudoCode = SB.ToString();
                                CurrentStep = HandleLogin__ToInjectRequestParametersStep;
                                ShowParameterUpdateQuestion(Questions[HandleLogin__ToInjectRequestParametersStep], false);
                                break;
                            default:
                                throw new Exception("Invalid Input, try again.");
                        }
                    }
                    catch { ShowError("Invalid Input, try again."); }
                    break;
                case (HandleLogin__IsPreLoginRequestNeededStep):
                    try
                    {
                        Answer = AnswerTB.Text.Trim();
                        switch (Answer)
                        {
                            case("1"):
                                CurrentStep = HandleLogin__PreLoginRequestSourceStep;
                                ShowRequestSourceQuestion(Questions[HandleLogin__PreLoginRequestSourceStep], "Pre Login");
                                break;
                            case ("2"):
                                StringBuilder SB = new StringBuilder();
                                SB.Append(LoginActionPseudoCode);
                                SB.Append(LoginRequestSourcePseudoCode);
                                LoginActionPseudoCode = SB.ToString();
                                CurrentStep = HandleLogin__LoginRequestParametersStep;
                                ShowParameterUpdateQuestion(Questions[HandleLogin__LoginRequestParametersStep], true);
                                break;
                            default:
                                throw new Exception("Invalid Input, try again.");
                        }
                    }
                    catch { ShowError("Invalid Input, try again."); }
                    break;
                case (HandleLogin__PreLoginRequestSourceStep):
                    try
                    {
                        string PreLoginRequestPseudoCode = RequestSourceToPseudoCode();
                        StringBuilder SB = new StringBuilder();
                        SB.Append(LoginActionPseudoCode);
                        SB.Append(PreLoginRequestPseudoCode);
                        SB.AppendLine("SendRequest");
                        SB.Append(LoginRequestSourcePseudoCode);
                        LoginActionPseudoCode = SB.ToString();
                        CurrentStep = HandleLogin__LoginRequestParametersStep;
                        ShowParameterUpdateQuestion(Questions[HandleLogin__LoginRequestParametersStep], false);
                    }
                    catch { ShowError("Invalid Input, try again."); }
                    break;
                case (HandleLogin__LoginRequestParametersStep):
                    try
                    {
                        string LoginRequestParametersUpdatePseudoCode = ParametersAnswerToPseudoCode();
                        StringBuilder SB = new StringBuilder();
                        SB.Append(LoginActionPseudoCode);
                        SB.Append(LoginRequestParametersUpdatePseudoCode);
                        LoginActionPseudoCode = SB.ToString();
                        CurrentStep = HandleLogin__ToInjectRequestParametersStep;
                        ShowParameterUpdateQuestion(Questions[HandleLogin__ToInjectRequestParametersStep], false);
                    }
                    catch{ShowError("Invalid Input, try again.");}
                    break;
                case (HandleLogin__ToInjectRequestParametersStep):
                    try
                    {
                        string PostLoginParametersUpdatePseudoCode = ParametersAnswerToPseudoCode();
                        StringBuilder SB = new StringBuilder();
                        SB.Append(LoginActionPseudoCode);
                        SB.AppendLine("SendRequest");
                        SB.AppendLine("GetRequest ToInject");
                        SB.Append(PostLoginParametersUpdatePseudoCode);
                        LoginActionPseudoCode = SB.ToString();
                        string[] Code = LoginActionPseudoCodeToCode();
                        CurrentStep = HandleLogin__ShowPseudoCodeStep;
                        //ShowQuestion(string.Format(Questions[HandleLogin__ShowPseudoCodeStep], LoginActionPseudoCode), true);
                        //ShowSelectOptionQuestion(string.Format(Questions[HandleLogin__ShowPseudoCodeStep], LoginActionPseudoCode), new List<string>() { }, "Go to Home Menu");
                        ShowPseudoCode(Questions[HandleLogin__ShowPseudoCodeStep], LoginActionPseudoCode);
                    }
                    catch { ShowError("Invalid Input, try again."); }
                    break;
                case(HandleLogin__ShowPseudoCodeStep):
                    GoToHomeMenu();
                    break;
            }
        }

        void ProcessHandleMultiStepAnswer()
        {
            string Answer = AnswerTB.Text.Trim();
            switch(CurrentStep)
            {
                case (HandleMultiStep__BaseStep):
                    switch (Answer)
                    {
                        case("1"):
                            CurrentStep = HandleMultiStep__BeforeAfterInjectionSelectionStep;
                            //ShowQuestion(Questions[HandleMultiStep__BeforeAfterInjectionSelectionStep], true);
                            ShowSelectOptionQuestion(Questions[HandleMultiStep__BeforeAfterInjectionSelectionStep], QuestionOptions[HandleMultiStep__BeforeAfterInjectionSelectionStep],  "");
                            break;
                        case ("2"):
                            GoToHomeMenu();
                            break;
                        case ("3"):
                            CurrentStep = HandleMultiStep__PseudoCodeStep;
                            ShowQuestion(Questions[HandleCSRFTokens__PseudoCodeStep], false);
                            break;
                        default:
                            ShowError("Invaid option entered, try again.");
                            break;
                    }
                    break;
                case (HandleMultiStep__PseudoCodeStep):
                    MultiStepActionPseudoCode = BigAnswerTB.Text.Trim();
                    MultiStepActionPseudoCodeToCode();
                    GoToHomeMenu();
                    break;
                case (HandleMultiStep__BeforeAfterInjectionSelectionStep):
                    Answer = AnswerTB.Text.Trim();
                    switch(Answer)
                    {
                        case("1"):
                            PreInjectionStepsPresent = true;
                            PostInjectionStepsPresent = false;
                            break;
                        case("2"):
                            PreInjectionStepsPresent = false;
                            PostInjectionStepsPresent = true;
                            break;
                        case("3"):
                            PreInjectionStepsPresent = true;
                            PostInjectionStepsPresent = true;
                            break;
                        default:
                            ShowError("Invaid option entered, try again.");
                            break;
                    }
                    MultiStepActionPseudoCode = "";
                    PreInjectionCounter = 0;
                    PreInjectionCounterMax = 0;
                    PostInjectionCounter = 0;
                    if (PreInjectionStepsPresent)
                        ShowPreInjectionRequestSourceQuestion();
                    else if (PostInjectionStepsPresent)
                        ShowPostInjectionRequestSourceQuestion();
                    break;
                case (HandleMultiStep__PreInjectionRequestSourceStep):
                    try
                    {
                        string PC = RequestSourceToPseudoCode();
                        StringBuilder SB = new StringBuilder();
                        SB.Append(MultiStepActionPseudoCode);
                        SB.Append(PC);
                        MultiStepActionPseudoCode = SB.ToString();
                        if (PreInjectionCounter == 1)
                        {
                            SB = new StringBuilder();
                            SB.Append(MultiStepActionPseudoCode);
                            SB.AppendLine("SendRequest");
                            MultiStepActionPseudoCode = SB.ToString();
                            CurrentStep = HandleMultiStep__MorePreInjectionRequestStep;
                            //ShowQuestion(Questions[HandleMultiStep__MorePreInjectionRequestStep], true);
                            ShowSelectOptionQuestion(Questions[HandleMultiStep__MorePreInjectionRequestStep], QuestionOptions[HandleMultiStep__MorePreInjectionRequestStep], "");
                        }
                        else
                        {
                            ShowPreInjectionParametersUpdateQuestions();
                        }
                    }
                    catch { ShowError("Invalid option, try again."); }
                    break;
                case (HandleMultiStep__PreInjectionRequestParametersStep):
                    try
                    {
                        string PC = ParametersAnswerToPseudoCode();
                        StringBuilder SB = new StringBuilder();
                        SB.Append(MultiStepActionPseudoCode);
                        if (PreInjectionCounterMax == PreInjectionCounter)
                        {
                            SB.AppendLine("GetRequest ToInject");
                        }
                        SB.Append(PC);
                        if (PreInjectionCounterMax < PreInjectionCounter)
                        {
                            SB.AppendLine("SendRequest");
                        }
                        MultiStepActionPseudoCode = SB.ToString();
                        if (PreInjectionCounterMax == PreInjectionCounter)
                        {
                            if (PostInjectionStepsPresent)
                            {
                                ShowPostInjectionRequestSourceQuestion();
                            }
                            else
                            {
                                CurrentStep = HandleMultiStep__ShowPseudoCodeStep;
                                //ShowQuestion(string.Format(Questions[HandleMultiStep__ShowPseudoCodeStep], MultiStepActionPseudoCode), true);
                                //ShowSelectOptionQuestion(string.Format(Questions[HandleMultiStep__ShowPseudoCodeStep], MultiStepActionPseudoCode), new List<string>() { }, "Go to Home Menu");
                                ShowPseudoCode(Questions[HandleMultiStep__ShowPseudoCodeStep], MultiStepActionPseudoCode);
                            }
                        }
                        else
                        {
                            CurrentStep = HandleMultiStep__MorePreInjectionRequestStep;
                            //ShowQuestion(Questions[HandleMultiStep__MorePreInjectionRequestStep], true);
                            ShowSelectOptionQuestion(Questions[HandleMultiStep__MorePreInjectionRequestStep], QuestionOptions[HandleMultiStep__MorePreInjectionRequestStep], "");
                        }
                    }
                    catch { ShowError("Invalid option, try again."); }
                    break;
                case (HandleMultiStep__MorePreInjectionRequestStep):
                    try
                    {
                        Answer = AnswerTB.Text.Trim();
                        switch (Answer)
                        {
                            case("1"):
                                ShowPreInjectionRequestSourceQuestion();
                                break;
                            case("2"):
                                PreInjectionCounterMax = PreInjectionCounter;
                                ShowPreInjectionParametersUpdateQuestions();
                                break;
                        }
                    }
                    catch { ShowError("Invalid option, try again"); }
                    break;
                case (HandleMultiStep__ToInjectRequestParametersStep):
                    break;
                case (HandleMultiStep__PostInjectionRequestSourceStep):
                    try
                    {
                        string PC = RequestSourceToPseudoCode();
                        StringBuilder SB = new StringBuilder();
                        SB.Append(MultiStepActionPseudoCode);
                        SB.Append(PC);
                        MultiStepActionPseudoCode = SB.ToString();
                        ShowPostInjectionParametersUpdateQuestions();
                    }
                    catch { ShowError("Invalid option, try again."); }
                    break;
                case (HandleMultiStep__PostInjectionRequestParametersStep):
                    try
                    {
                        string PC = ParametersAnswerToPseudoCode();
                        StringBuilder SB = new StringBuilder();
                        SB.Append(MultiStepActionPseudoCode);
                        SB.Append(PC);
                        SB.AppendLine("SendRequest");
                        MultiStepActionPseudoCode = SB.ToString();
                        CurrentStep = HandleMultiStep__MorePostInjectionRequestStep;
                        //ShowQuestion(Questions[HandleMultiStep__MorePostInjectionRequestStep], true);
                        ShowSelectOptionQuestion(Questions[HandleMultiStep__MorePostInjectionRequestStep], QuestionOptions[HandleMultiStep__MorePostInjectionRequestStep], "");
                    }
                    catch { ShowError("Invalid option, try again."); }
                    break;
                case (HandleMultiStep__MorePostInjectionRequestStep):
                    try
                    {
                        Answer = AnswerTB.Text.Trim();
                        switch (Answer)
                        {
                            case ("1"):
                                ShowPostInjectionRequestSourceQuestion();
                                break;
                            case ("2"):
                                CurrentStep = HandleMultiStep__ShowPseudoCodeStep;
                                //ShowQuestion(string.Format(Questions[HandleMultiStep__ShowPseudoCodeStep], MultiStepActionPseudoCode), true);
                                //ShowSelectOptionQuestion(string.Format(Questions[HandleMultiStep__ShowPseudoCodeStep], MultiStepActionPseudoCode), new List<string>() { }, "Go to Home Menu");
                                ShowPseudoCode(Questions[HandleMultiStep__ShowPseudoCodeStep], MultiStepActionPseudoCode);
                                break;
                        }
                    }
                    catch { ShowError("Invalid option, try again"); }
                    break;
                case (HandleMultiStep__ShowPseudoCodeStep):
                    GoToHomeMenu();
                    break;
            }
        }

        void ProcessHandleSetCookiesAnswer()
        {
            string Answer = "";
            switch (CurrentStep)
            {
                case (HandleSetCookies__BaseStep):
                    Answer = AnswerTB.Text.Trim();
                    switch (Answer)
                    {
                        case("1"):
                            CurrentStep = HandleSetCookies__ParameterNames;
                            ShowQuestion(Questions[HandleSetCookies__ParameterNames], false);
                            break;
                        case ("2"):
                            GoToHomeMenu();
                            break;
                        default:
                            ShowError("Invalid option, try again");
                            break;
                    }
                    break;
                case (HandleSetCookies__ParameterNames):
                    Answer = BigAnswerTB.Text.Trim();
                    try
                    {
                        string[] ParameterNames = Answer.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string _PN in ParameterNames)
                        {
                            string PN = _PN.Trim();
                            if (PN.Length > 0) NamesOfCookieParametersToUpdate.Add(PN);
                        }
                    }
                    catch { }
                    GoToHomeMenu();
                    break;
            }
        }

        void ProcessPluginCreationAnswer()
        {
            string Answer = AnswerTB.Text.Trim();
            switch (CurrentStep)
            {
                case (PluginCreation__PluginNameStep):
                    if(!Regex.IsMatch(Answer, "^[a-zA-Z]+$"))
                    {
                        ShowError("Plugin Name should only contain alphabets (a-z)");
                        return;
                    }
                    if (!Answer[0].ToString().ToUpper().Equals(Answer[0].ToString()))
                    {
                        ShowError("Plugin Name should begin with an upper case letter");
                        return;
                    }
                    if (SessionPlugin.List().Contains(Answer))
                    {
                        ShowError("A Session Plugin with this name already exists. Please select a different name.");
                        return;
                    }
                    PluginName = Answer;
                    CurrentStep = PluginCreation__LanguageStep;
                    ShowSelectOptionQuestion(Questions[PluginCreation__LanguageStep], QuestionOptions[PluginCreation__LanguageStep], "");
                    //ShowQuestion(Questions[PluginCreation__LanguageStep], true);
                    break;
                case (PluginCreation__LanguageStep):
                    switch(Answer)
                    {
                        case("1"):
                            PluginLang = "py";
                            break;
                        case ("2"):
                            PluginLang = "rb";
                            break;
                        default:
                            ShowError("Invalid option, try again.");
                            return;
                    }
                    try
                    {
                        string[] Code = PseudoCodeToPlugin();
                        string PluginCode = "";
                        if (PluginLang.Equals("py"))
                            PluginCode = Code[0];
                        else
                            PluginCode = Code[1];
                        bool PluginCreated = false;
                        
                        int Counter = 0;
                        string FFN = "";
                        while (!PluginCreated)
                        {
                            string FN = "";
                            if (Counter == 0)
                                FN = string.Format("{0}.{1}", PluginName, PluginLang);
                            else
                                FN = string.Format("{0}_{1}.{2}", PluginName, Counter, PluginLang);
                            FFN = string.Format("{0}\\Plugins\\Session\\{1}", Config.Path, FN);
                            Counter++;
                            if (!File.Exists(FFN))
                            {
                                try
                                {
                                    File.WriteAllText(FFN, PluginCode);
                                    PluginCreated = true;
                                    PluginStore.LoadNewSessionPlugins();
                                }
                                catch (Exception Exp) { ShowError(string.Format("Unable to create plugin file - {0}", Exp.Message)); }
                            }
                        }
                        CurrentStep = PluginCreation__FinalStep;
                        ShowSelectOptionQuestion(string.Format(Questions[PluginCreation__FinalStep], FFN), new List<string>(), "Close this Assistant");
                    }
                    catch (Exception Exp) { ShowError(string.Format("Error creating plugin - {0}", Exp.Message));}
                    break;
                case (PluginCreation__FinalStep):
                    this.Close();
                    break;
            }
        }
        #endregion


        #region misc
        void GoToHomeMenu()
        {
            CurrentSection = HomeMenu;
            CurrentStep = HomeMenu__BaseStep;
            ShowSelectOptionQuestion(Questions[HomeMenu__BaseStep], QuestionOptions[HomeMenu__BaseStep], "Create a Session Plugin based on the answers provided");
            //ShowQuestion(Questions[HomeMenu__BaseStep], true);
        }

        string GetLogSourcesList()
        {
            if (LogSources.Keys.Count == 0)
            {
                LogSources.Add(1, "Proxy");
                LogSources.Add(2, "Probe");
                LogSources.Add(3, "Test");
                LogSources.Add(4, "Shell");
                LogSources.Add(5, "Scan");
                List<string> Sources = Config.GetOtherSourceList();
                for (int i = 0; i < Sources.Count; i++)
                {
                    LogSources.Add(i + 6, Sources[i]);
                }
            }
            StringBuilder SB = new StringBuilder();
            foreach (int Index in LogSources.Keys)
            {
                SB.AppendLine(string.Format("{0}) {1} Log", Index, LogSources[Index]));
            }
            return SB.ToString();
        }

        void ResetParameterAnswerFields(bool ClearGridRows)
        {
            ParameterTypeCombo.SelectedIndex = 1;
            ParameterNameTB.Text = "";
            ParameterSourceFromResponseRB.Checked = false;
            ParameterSourceFromUserRB.Checked = false;
            ParseParameterFromHtmlRB.Checked = false;
            ParseParameterFromRegexRB.Checked = false;
            ParameterParseRegexTB.Text = "";
            ParameterSourceFromUserRB.Checked = false;
            ParameterAskUserHintTB.Text = "";
            HowToParseResponsePanel.Visible = false;
            HowToUpdateParameterPanel.Visible = false;
            UserHintPanel.Visible = false;
            AddParameterAnswerEntryLL.Visible = false;
            EditParameterAnswerEntryLL.Enabled = false;
            DeleteParameterAnswerEntryLL.Enabled = false;
            EditParameterAnswerEntryLL.Visible = false;
            DeleteParameterAnswerEntryLL.Visible = false;
            if (ClearGridRows)
                ParametersAnswerGrid.Rows.Clear();
        }

        string GetNumberWithSuffix(int Num)
        {
            if(Num > 10 && Num < 20)
            {
                return string.Format("{0}th", Num);
            }
            else
            {
                string NumStr = Num.ToString();
                switch(NumStr[NumStr.Length - 1])
                {
                    case('1'):
                        return string.Format("{0}st", Num);
                    case ('2'):
                        return string.Format("{0}nd", Num);
                    case ('3'):
                        return string.Format("{0}rd", Num);
                    default:
                        return string.Format("{0}th", Num);
                }
            }
        }
        #endregion

        #region CreatePseudoCode
        string RequestSourceToPseudoCode()
        {
            string LogSource = RequestSourceCombo.Text;
            string LogId = RequestSourceIdTB.Text.Trim();
            string RequestName = string.Format("{0} request", RequestSourceNameTB.Text.Trim());
            this.RequestNamesFromUserList.Add(RequestName);
            int RequestId = Int32.Parse(LogId);
            return string.Format("GetRequest[{0}] {1}-{2}{3}", RequestName, LogSource, RequestId, Environment.NewLine);
        }

        string RedirectSignatureToPseudoCode()
        {
            string PartialPseudoCode = "";
            string Code = SignatureResponseCodeTB.Text;
            string Location = LocationSignatureTypeSelectCombo.Text;
            string Keyword = LocationSignatureKeywordTB.Text;
            if (UseLocationSignatureCB.Checked)
            {
                PartialPseudoCode = SignatureToPartialPseudoCode(Code, Location, Keyword, "Location");
            }
            else
            {
                PartialPseudoCode = SignatureToPartialPseudoCode(Code, "", "", "");
            }
            return string.Format("FollowRedirect {0}", PartialPseudoCode);
        }

        string ResponseSignatureToPseudoCode()
        {
            string Code = SignatureResponseCodeTB.Text;
            string Location = LocationSignatureTypeSelectCombo.Text;
            string Keyword = LocationSignatureKeywordTB.Text;
            string PartialPseudoCode = "";
            StringBuilder SB = new StringBuilder();
            
            if (LoggedInResponseSignatureRB.Checked)
                SB.AppendLine("LoggedInSignatureBegins");
            else if (LoggedOutResponseSignatureRB.Checked)
                SB.AppendLine("LoggedOutSignatureBegins");
            else
                throw new Exception("Invalid input");

            if (UseLocationSignatureCB.Checked)
            {
                PartialPseudoCode = SignatureToPartialPseudoCode(Code, Location, Keyword, "Location");
                SB.AppendLine(string.Format("LastResponse {0}", PartialPseudoCode));
            }
            if (UseTitleSignatureCB.Checked)
            {
                Location = TitleSignatureTypeSelectCombo.Text;
                Keyword = TitleSignatureKeywordTB.Text;
                PartialPseudoCode = SignatureToPartialPseudoCode(Code, Location, Keyword, "Title");
                SB.AppendLine(string.Format("LastResponse {0}", PartialPseudoCode));
            }
            if (UseBodySignatureCB.Checked)
            {
                Location = BodySignatureTypeSelectCombo.Text;
                Keyword = BodySignatureKeywordTB.Text;
                PartialPseudoCode = SignatureToPartialPseudoCode(Code, Location, Keyword, "Body");
                SB.AppendLine(string.Format("LastResponse {0}", PartialPseudoCode));
            }
            
            if (LoggedInResponseSignatureRB.Checked)
                SB.AppendLine("LoggedInSignatureEnds");
            else if (LoggedOutResponseSignatureRB.Checked)
                SB.AppendLine("LoggedOutSignatureEnds");
            else
                throw new Exception("Invalid input");

            return SB.ToString();
        }

        string SignatureToPartialPseudoCode(string ResponseCode, string Location, string Keyword, string Section)
        {
            int Code = Int32.Parse(ResponseCode);
            string Prefix = "";
            string PartialPseudoCode = string.Format("Code {0}", Code);
            if (Location.Length == 0 && Keyword.Length == 0 && Section.Length == 0) return PartialPseudoCode;
            
            switch (Location)
            {
                case ("starts with"):
                    Prefix = "StartsWith";
                    break;
                case ("ends with"):
                    Prefix = "EndsWith";
                    break;
                case ("contains"):
                    Prefix = "Has";
                    break;
                case ("does not contain"):
                    Prefix = "NotHas";
                    break;
                case ("matches regex"):
                    Prefix = "Regex";
                    try
                    {
                        new Regex(Keyword);
                    }
                    catch
                    {
                        throw new Exception("Invalid Regex entered");
                    }
                    Keyword = string.Format("/{0}/", Keyword);
                    break;
                default:
                    throw new Exception("Invalid Location");
            }
            PartialPseudoCode = string.Format("{0} {1} {2} {3}", PartialPseudoCode, Section, Prefix, Keyword);
            return PartialPseudoCode;
        }

        string ParametersAnswerToPseudoCode()
        {
            List<string[]> Answers = new List<string[]>();
            foreach (DataGridViewRow Row in ParametersAnswerGrid.Rows)
            {
                string[] Fields = new string[6];
                Fields[0] = Row.Cells["ParameterSectionColumn"].Value.ToString();
                Fields[1] = Row.Cells["ParameterNameColumn"].Value.ToString();
                Fields[2] = Row.Cells["UpdateFromColumn"].Value.ToString();
                Fields[3] = Row.Cells["ParseResponseColumn"].Value.ToString();
                Fields[4] = Row.Cells["RegexColumn"].Value.ToString();
                Fields[5] = Row.Cells["HintColumn"].Value.ToString();
                Answers.Add(Fields);
            }
            
            StringBuilder SB = new StringBuilder();
            foreach(string[] Answer in Answers)
            {
                string ParameterSection = Answer[0];
                string ParameterName = Answer[1];
                string UpdateFrom = Answer[2];
                string ParseMode = Answer[3];
                string RegexString = Answer[4];
                string Hint = Answer[5];
                SB.Append(string.Format("UpdateRequest {0} {1} From ", ParameterSection, ParameterName));
                if(UpdateFrom.Equals("Response"))
                {
                    SB.Append("LastResponse ");
                    if(ParseMode.Equals("Html Fields"))
                        SB.Append("Html");
                    else
                        SB.Append(string.Format("Regex /{0}/", RegexString));             
                }
                else
                {
                    SB.Append(string.Format("User Hint {0}", Hint));
                }
                SB.AppendLine();
            }
            return SB.ToString();
        }
        #endregion

        #region CreatePlugin

        int UpdateToInjectRequestMethodCounter = 0;
        Dictionary<int, string> UpdateToInectRequestMethodPyDeclarations = new Dictionary<int, string>();
        Dictionary<int, string> UpdateToInectRequestMethodRbDeclarations = new Dictionary<int, string>();

        string[] PseudoCodeToPlugin()
        {
            string[] Code = new string[2];

            StringBuilder Py = new StringBuilder();
            StringBuilder Rb = new StringBuilder();

            StringBuilder PyMiddle = new StringBuilder();
            StringBuilder RbMiddle = new StringBuilder();

            string[] DoBeforeInjectionCode = PseudoCodeToDoBeforeInjectionCode();
            PyMiddle.Append(DoBeforeInjectionCode[0]);
            RbMiddle.Append(DoBeforeInjectionCode[1]);

            string[] DoAfterInjectionCode = PseudoCodeToDoAfterInjectionCode();
            PyMiddle.Append(DoAfterInjectionCode[0]);
            RbMiddle.Append(DoAfterInjectionCode[1]);

            if (NamesOfCookieParametersToUpdate.Count > 0)
            {
                string[] NamesOfCookieParametersToUpdateListCode = NamesOfCookieParametersToUpdateListToCode();
                PyMiddle.Append(NamesOfCookieParametersToUpdateListCode[0]);
                RbMiddle.Append(NamesOfCookieParametersToUpdateListCode[1]);
            }
            if (FollowRedirectPseudoCode.Length > 0)
            {
                string[] FollowRediretCode = FollowRediretPseudoCodeToCode();
                PyMiddle.Append(FollowRediretCode[0]);
                RbMiddle.Append(FollowRediretCode[1]);
            }
            if (LoginActionPseudoCode.Length > 0)
            {
                string[] LoginActionCode = LoginActionPseudoCodeToCode();
                PyMiddle.Append(LoginActionCode[0]);
                RbMiddle.Append(LoginActionCode[1]);
            }
            if (MultiStepActionPseudoCode.Length > 0)
            {
                string[] MultiStepActionCode = MultiStepActionPseudoCodeToCode();
                PyMiddle.Append(MultiStepActionCode[0]);
                RbMiddle.Append(MultiStepActionCode[1]);
            }
            if (UpdateCSRFTokensPseudoCode.Length > 0)
            {
                string[] UpdateCSRFTokensCode = UpdateCSRFTokensPseudoCodeToCode();
                PyMiddle.Append(UpdateCSRFTokensCode[0]);
                RbMiddle.Append(UpdateCSRFTokensCode[1]);
            }

            //Declare the update_req method
            PyMiddle.AppendLine();
            PyMiddle.Append("\t"); PyMiddle.AppendLine("def update_req(self, req):");            
            if (UpdateToInectRequestMethodPyDeclarations.Count > 0)
            {
                PyMiddle.Append("\t\t"); PyMiddle.AppendLine("if Tools.MD5(req.ToString()) != self.last_main_request_signature:");
                for (int i = 1; i <= UpdateToInectRequestMethodPyDeclarations.Count; i++)
                {
                    PyMiddle.Append("\t\t\t"); PyMiddle.AppendLine(string.Format("req = self.update_req_{0}(req)", i));
                }
            }
            else
            {
                PyMiddle.AppendLine("\t\t#no update");
                PyMiddle.Append("\t\t"); PyMiddle.AppendLine("return req");
            }

            RbMiddle.AppendLine();
            RbMiddle.Append("\t"); RbMiddle.AppendLine("def update_req(req)");
            if (UpdateToInectRequestMethodRbDeclarations.Count > 0)
            {
                RbMiddle.Append("\t\t"); RbMiddle.AppendLine("if Tools.md5(req.to_string) != @last_main_request_signature");
                for (int i = 1; i <= UpdateToInectRequestMethodRbDeclarations.Count; i++)
                {
                    RbMiddle.Append("\t\t\t"); RbMiddle.AppendLine(string.Format("req = update_req_{0}(req)", i));
                }
                RbMiddle.Append("\t\t"); RbMiddle.AppendLine("end");
            }
            else
            {
                RbMiddle.AppendLine("\t\t#no update");
                RbMiddle.Append("\t\t"); RbMiddle.AppendLine("return req");
            }
            RbMiddle.Append("\t"); RbMiddle.AppendLine("end");

            //Declare other update_req_ methods
            foreach (string MethodDec in UpdateToInectRequestMethodPyDeclarations.Values)
            {
                PyMiddle.AppendLine();
                PyMiddle.Append(MethodDec);
                PyMiddle.AppendLine();
            }
            foreach (string MethodDec in UpdateToInectRequestMethodRbDeclarations.Values)
            {
                RbMiddle.AppendLine();
                RbMiddle.Append(MethodDec);
                RbMiddle.AppendLine();
            }

            //Declare the get_ask_user_message method
            PyMiddle.AppendLine();            
            PyMiddle.Append("\t"); PyMiddle.AppendLine("def get_ask_user_message_format(self, param, section, hint):");
            PyMiddle.Append("\t\t"); PyMiddle.AppendLine("return 'Enter the value of the ' + param + ' parameter in the ' + section + ' section of the Request. \\r\\nHint:\\r\\n' + hint");
            
            RbMiddle.AppendLine();
            RbMiddle.Append("\t"); RbMiddle.AppendLine("def get_ask_user_message_format(param, section, hint)");
            RbMiddle.Append("\t\t"); RbMiddle.AppendLine(@"return 'Enter the value of the ' + param + ' parameter in the ' + section + "" section of the Request. \r\nHint:\r\n"" + hint");
            RbMiddle.Append("\t"); RbMiddle.AppendLine("end");


            StringBuilder PyTop = new StringBuilder();
            StringBuilder RbTop = new StringBuilder();
            PyTop.AppendLine("from IronWASP import *");
            PyTop.AppendLine("from System import *");
            PyTop.AppendLine("import re");

            RbTop.AppendLine("include IronWASP");


            if (PluginName.Length == 0) throw new Exception("Plugin Name is missing");
            //Declare the new Session Plugin class method
            PyTop.AppendLine();            
            PyTop.AppendLine(string.Format("class {0}(SessionPlugin):", PluginName));
            PyTop.AppendLine();

            RbTop.AppendLine();
            RbTop.AppendLine(string.Format("class {0} < SessionPlugin", PluginName));
            RbTop.AppendLine();

            //Declare the GetInstance method
            PyTop.Append("\t"); PyTop.AppendLine("def GetInstance(self):");
            PyTop.Append("\t\t"); PyTop.AppendLine(string.Format("p = {0}()", PluginName));
            PyTop.Append("\t\t"); PyTop.AppendLine(string.Format("p.Name = '{0}'", PluginName));
            PyTop.Append("\t\t"); PyTop.AppendLine("p.init_instance_variables()");
            PyTop.Append("\t\t"); PyTop.AppendLine("return p");
            PyTop.AppendLine();

            RbTop.AppendLine();
            RbTop.Append("\t"); RbTop.AppendLine("def GetInstance()");
            RbTop.Append("\t\t"); RbTop.AppendLine(string.Format("p = {0}.new", PluginName));
            RbTop.Append("\t\t"); RbTop.AppendLine(string.Format("p.name = '{0}'", PluginName));
            RbTop.Append("\t\t"); RbTop.AppendLine("p.init_instance_variables");
            RbTop.Append("\t\t"); RbTop.AppendLine("return p");
            RbTop.Append("\t"); RbTop.AppendLine("end");
            RbTop.AppendLine();

            //Declare the init_instance_variables method
            PyTop.Append("\t"); PyTop.AppendLine("def init_instance_variables(self):");
            PyTop.Append("\t\t"); PyTop.AppendLine("self.cookie_store = CookieStore()");
            PyTop.Append("\t\t"); PyTop.AppendLine("self.reqs = {}");
            foreach (string LabelName in RequestDeclarationsInCode.Keys)
            {
                foreach (string RequestName in RequestDeclarationsInCode[LabelName].Keys)
                {
                    PyTop.Append("\t\t"); PyTop.AppendLine(string.Format("self.reqs['{0}'] = {1}", RequestName, RequestDeclarationsInCode[LabelName][RequestName]));
                }
            }
            PyTop.Append("\t\t"); PyTop.AppendLine("self.ress = {}");
            PyTop.Append("\t\t"); PyTop.AppendLine("self.last_main_request_signature = ''");
            
            RbTop.Append("\t"); RbTop.AppendLine("def init_instance_variables()");
            RbTop.Append("\t\t"); RbTop.AppendLine("@cookie_store = CookieStore.new");
            RbTop.Append("\t\t"); RbTop.AppendLine("@reqs = {}");
            foreach (string LabelName in RequestDeclarationsInCode.Keys)
            {
                foreach (string RequestName in RequestDeclarationsInCode[LabelName].Keys)
                {
                    RbTop.Append("\t\t"); RbTop.AppendLine(string.Format("@reqs['{0}'] = {1}", RequestName, RequestDeclarationsInCode[LabelName][RequestName]));
                }
            }
            RbTop.Append("\t\t"); RbTop.AppendLine("@ress = {}");
            RbTop.Append("\t\t"); RbTop.AppendLine("@last_main_request_signature = ''");
            RbTop.Append("\t"); RbTop.AppendLine("end");


            StringBuilder PyBottom = new StringBuilder();
            StringBuilder RbBottom = new StringBuilder();
            //end statement for the class declaration
            RbBottom.AppendLine("end");
            RbBottom.AppendLine(); RbBottom.AppendLine();

            //Add a instance of this SessionPlugin class to the list of Session Plugins
            PyBottom.AppendLine(string.Format("p = {0}()", PluginName));
            PyBottom.AppendLine("SessionPlugin.Add(p.GetInstance())");

            RbBottom.AppendLine(string.Format("p = {0}.new", PluginName));
            RbBottom.AppendLine("SessionPlugin.add(p.get_instance)");

            List<string> Comments = GetPSeudoCodeAsComments();
            PyBottom.AppendLine();
            RbBottom.AppendLine();
            foreach (string Comment in Comments)
            {
                PyBottom.AppendLine(Comment);
                RbBottom.AppendLine(Comment);
            }

            //Putting it all together
            Py.Append(PyTop.ToString());
            Py.AppendLine();
            Py.Append(PyMiddle.ToString());
            Py.AppendLine();
            Py.Append(PyBottom.ToString());



            Rb.Append(RbTop.ToString());
            Rb.AppendLine();
            Rb.Append(RbMiddle.ToString());
            Rb.AppendLine();
            Rb.Append(RbBottom.ToString());

            Code[0] = Py.ToString();
            Code[1] = Rb.ToString();
            return Code;
        }

        List<string> GetPSeudoCodeAsComments()
        {
            List<string> Comments = new List<string>();
            Comments.Add("#PseudoCode used to generate this Plugin:");
            Comments.Add("#");
            Comments.Add("#");
            if (NamesOfCookieParametersToUpdate.Count > 0)
            {
                Comments.Add("#");
                Comments.Add("#Cookie parameter that will be updated from the Response of the 'Main Request':");
                Comments.Add("#");
                foreach (string Cookie in NamesOfCookieParametersToUpdate)
                {
                    Comments.Add(string.Format("#{0}", Cookie));
                }
            }
            Comments.Add("#");
            if (UpdateCSRFTokensPseudoCode.Length > 0)
            {
                Comments.Add("#");
                Comments.Add("#Update CSRF token action PseudoCode:");
                Comments.Add("#");
                string[] Lines = UpdateCSRFTokensPseudoCode.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string Line in Lines)
                {
                    Comments.Add(string.Format("#{0}", Line));
                }
            }
            Comments.Add("#");
            if (LoginActionPseudoCode.Length > 0)
            {
                Comments.Add("#");
                Comments.Add("#Login action PseudoCode:");
                Comments.Add("#");
                string[] Lines = LoginActionPseudoCode.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string Line in Lines)
                {
                    Comments.Add(string.Format("#{0}", Line));
                }
            }
            Comments.Add("#");
            if (FollowRedirectPseudoCode.Length > 0)
            {
                Comments.Add("#");
                Comments.Add("#Follow Redirect PseudoCode:");
                Comments.Add("#");
                string[] Lines = FollowRedirectPseudoCode.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string Line in Lines)
                {
                    Comments.Add(string.Format("#{0}", Line));
                }
            }
            Comments.Add("#");
            if (MultiStepActionPseudoCode.Length > 0)
            {
                Comments.Add("#");
                Comments.Add("#Multi-step Form Submission PseudoCode:");
                Comments.Add("#");
                string[] Lines = MultiStepActionPseudoCode.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string Line in Lines)
                {
                    Comments.Add(string.Format("#{0}", Line));
                }
            }
            Comments.Add("#");
            return Comments;
        }

        string[] PseudoCodeToDoBeforeInjectionCode()
        {
            string[] Code = new string[2];
            StringBuilder Py = new StringBuilder();
            StringBuilder Rb = new StringBuilder();

            Py.AppendLine();
            Py.Append("\t"); Py.AppendLine("def DoBeforeSending(self, req, res):");
            Py.Append("\t\t"); Py.AppendLine("self.Trace(req, 'Preparing \\'Main Request\\' before it is sent', 'The following request is the \\'Main Request\\'\\r\\n\\r\\n\\r\\n:' + req.ToString())");
            Py.Append("\t\t"); Py.AppendLine("req.SetCookie(self.cookie_store)");
            Py.Append("\t\t"); Py.AppendLine("self.update_req(req)");

            Rb.AppendLine();
            Rb.Append("\t"); Rb.AppendLine("def DoBeforeSending(req, res)");
            Rb.Append("\t\t"); Rb.AppendLine(@"Trace(req, ""Preparing 'Main Request' before it is sent"", ""The following request is the 'Main Request' \r\n\r\n\r\n:"" + req.to_string)");
            Rb.Append("\t\t"); Rb.AppendLine("req.set_cookie(@cookie_store)");
            Rb.Append("\t\t"); Rb.AppendLine("update_req(req)");

            if (LoginActionPseudoCode.Length > 0)
            {
                Py.Append("\t\t"); Py.AppendLine("if not self.is_logged_in():");
                Py.Append("\t\t\t"); Py.AppendLine("self.login_user(req)");

                Rb.Append("\t\t"); Rb.AppendLine("if not is_logged_in");
                Rb.Append("\t\t\t"); Rb.AppendLine("login_user(req)");
                Rb.Append("\t\t"); Rb.AppendLine("end");
            }
            if (MultiStepActionPseudoCode.Length > 0)
            {
                Py.Append("\t\t"); Py.AppendLine("req = self.multi_step_before_sending(req)");

                Rb.Append("\t\t"); Rb.AppendLine("req = multi_step_before_sending(req)");
            }
            if (UpdateCSRFTokensPseudoCode.Length > 0)
            {
                Py.Append("\t\t"); Py.AppendLine("req = self.update_csrf_tokens(req)");

                Rb.Append("\t\t"); Rb.AppendLine("req = update_csrf_tokens(req)");
            }
            Py.Append("\t\t"); Py.AppendLine("self.last_main_request_signature = Tools.MD5(req.ToString())");
            Py.Append("\t\t"); Py.AppendLine("return req");
            Py.AppendLine();

            Rb.Append("\t\t"); Rb.AppendLine("@last_main_request_signature = Tools.md5(req.to_string)");
            Rb.Append("\t\t"); Rb.AppendLine("return req");
            Rb.Append("\t"); Rb.AppendLine("end");
            Rb.AppendLine();


            Code[0] = Py.ToString();
            Code[1] = Rb.ToString();
            return Code;
        }
        string[] PseudoCodeToDoAfterInjectionCode()
        {
            string[] Code = new string[2];
            StringBuilder Py = new StringBuilder();
            StringBuilder Rb = new StringBuilder();
            
            Py.AppendLine();
            Py.Append("\t"); Py.AppendLine("def DoAfterSending(self, res, req):");
            Py.Append("\t\t"); Py.AppendLine("self.Trace(req, '\\'Main Request\\' sent successfully and got the response', 'The \\'Main Request\\' has been sent to the server successfully and a response has been recieved. Click on the load request/response button to view the Main Request and its response.')");

            Rb.AppendLine();
            Rb.Append("\t"); Rb.AppendLine("def DoAfterSending(res, req)");
            Rb.Append("\t\t"); Rb.AppendLine("Trace(req, '\\'Main Request\\' sent successfully and got the response', 'The \\'Main Request\\' has been sent to the server successfully and a response has been recieved. Click on the load request/response button to view the Main Request and its response.')");

            string ResponseToReturn = "res";
            if (NamesOfCookieParametersToUpdate.Count > 0)
            {
                Py.Append("\t\t"); Py.AppendLine("self.read_set_cookies(req, res)");

                Rb.Append("\t\t"); Rb.AppendLine("read_set_cookies(req, res)");
            }
            if (MultiStepActionPseudoCode.Length > 0)
            {
                Py.Append("\t\t"); Py.AppendLine("res_after_multi_step = self.multi_step_after_sending(res, req)");

                Rb.Append("\t\t"); Rb.AppendLine("res_after_multi_step = multi_step_after_sending(res, req)");

                ResponseToReturn = "res_after_multi_step";
            }
            if (FollowRedirectPseudoCode.Length > 0)
            {
                Py.Append("\t\t"); Py.AppendLine(string.Format("res_after_follow_redirect = self.follow_redirect(req, {0})", ResponseToReturn));

                Rb.Append("\t\t"); Rb.AppendLine(string.Format("res_after_follow_redirect = follow_redirect(req, {0})", ResponseToReturn));

                ResponseToReturn = "res_after_follow_redirect";
            }
            Py.Append("\t\t"); Py.AppendLine(string.Format("return {0}", ResponseToReturn));
            Py.AppendLine();

            Rb.Append("\t\t"); Rb.AppendLine(string.Format("return {0}", ResponseToReturn));
            Rb.Append("\t"); Rb.AppendLine("end");
            Rb.AppendLine();

            Code[0] = Py.ToString();
            Code[1] = Rb.ToString();
            return Code;
        }

        //Target code reference:
        //Python
        //def follow_redirect(req, res):
        //    if res.Code == 302:
        //      if res.Headers.Get("Location").startswith("./abcd/"):
        //          return req.Follow(res)
        //    return res
        string[] NamesOfCookieParametersToUpdateListToCode()
        {
            string[] Code = new string[2];
            StringBuilder Py = new StringBuilder();
            StringBuilder Rb = new StringBuilder();

            Py.AppendLine();
            Py.Append("\t"); Py.AppendLine("def read_set_cookies(self, req, res):");

            Rb.AppendLine();
            Rb.Append("\t"); Rb.AppendLine("def read_set_cookies(req, res)");

            StringBuilder NamesList = new StringBuilder();
            foreach (string CookieName in NamesOfCookieParametersToUpdate)
            {
                NamesList.Append("'");NamesList.Append(CookieName.Replace("'", "\'"));NamesList.Append("'");NamesList.Append(",");
            }
            Py.Append("\t\t"); Py.AppendLine(string.Format("cookies_to_update = [{0}]", NamesList.ToString().TrimEnd(new char[]{','})));
            Py.Append("\t\t"); Py.AppendLine("for sc in res.SetCookies:");
            Py.Append("\t\t\t"); Py.AppendLine("if cookies_to_update.count(sc.Name) > 0 and len(sc.Value) > 0:");
            Py.Append("\t\t\t\t"); Py.AppendLine("self.cookie_store.Add(req, sc)");
            Py.AppendLine();

            Rb.Append("\t\t"); Rb.AppendLine(string.Format("cookies_to_update = [{0}]", NamesList.ToString().TrimEnd(new char[] { ',' })));
            Rb.Append("\t\t"); Rb.AppendLine("for sc in res.set_cookies");
            Rb.Append("\t\t\t"); Rb.AppendLine("if cookies_to_update.index(sc.name) and sc.value.length > 0");
            Rb.Append("\t\t\t\t"); Rb.AppendLine("@cookie_store.add(req, sc)");
            Rb.Append("\t\t\t"); Rb.AppendLine("end");
            Rb.Append("\t\t"); Rb.AppendLine("end");
            Rb.Append("\t"); Rb.AppendLine("end");
            Rb.AppendLine();

            Code[0] = Py.ToString();
            Code[1] = Rb.ToString();
            return Code;
        }
        string[] FollowRediretPseudoCodeToCode()
        {
            string[] Code = new string[2];
            StringBuilder Py = new StringBuilder();
            StringBuilder Rb = new StringBuilder();
            Regex FullFollowRedirectRegex = new Regex(@"FollowRedirect Code (\d{3}) Location (StartsWith|EndsWith|Has|NotHas|Regex) (.+)");
            Regex CodeOnlyFollowRedirectRegex = new Regex(@"FollowRedirect Code (\d{3})");
            string ResponseCode = "";
            string MatchType = "";
            string Keyword = "";
            
            Match M = FullFollowRedirectRegex.Match(FollowRedirectPseudoCode);
            if(M.Success)
            {
                ResponseCode = M.Groups[1].Value;
                MatchType = M.Groups[2].Value;
                Keyword = M.Groups[3].Value;
            }
            else
            {
                M = CodeOnlyFollowRedirectRegex.Match(FollowRedirectPseudoCode);
                if(M.Success)
                {
                    ResponseCode = M.Groups[1].Value;
                }
                else
                {
                    throw new Exception("Invalid Pseudo Code");
                }
            }

            Py.AppendLine();
            Py.Append("\t"); Py.AppendLine("def follow_redirect(self, req, res):");
            Py.Append("\t\t"); Py.AppendLine(string.Format("if res.Headers.Has('Location'):", ResponseCode));

            Rb.AppendLine();
            Rb.Append("\t"); Rb.AppendLine("def follow_redirect(req, res)");
            Rb.Append("\t\t"); Rb.AppendLine(string.Format("if res.headers.has('Location')", ResponseCode));

            if (MatchType.Length > 0 && Keyword.Length > 0)
            {
                switch (MatchType)
                {
                    case ("StartsWith"):
                        Py.Append("\t\t\t"); Py.AppendLine(string.Format("if res.Headers.Get('Location').startswith('{0}'):",Keyword));
                        Rb.Append("\t\t\t"); Rb.AppendLine(string.Format("if res.headers.get('Location').start_with?('{0}')",Keyword));
                        break;
                    case ("EndsWith"):
                        Py.Append("\t\t\t"); Py.AppendLine(string.Format("if res.Headers.Get('Location').endswith('{0}'):",Keyword));
                        Rb.Append("\t\t\t"); Rb.AppendLine(string.Format("if res.headers.get('Location').end_with?('{0}')",Keyword));
                        break;
                    case ("Has"):
                        Py.Append("\t\t\t"); Py.AppendLine(string.Format("if res.Headers.Get('Location').count('{0}') > 0:",Keyword));
                        Rb.Append("\t\t\t"); Rb.AppendLine(string.Format("if res.headers.get('Location').index('{0}')",Keyword));
                        break;
                    case ("NotHas"):
                        Py.Append("\t\t\t"); Py.AppendLine(string.Format("if res.Headers.Get('Location').count('{0}') == 0:",Keyword));
                        Rb.Append("\t\t\t"); Rb.AppendLine(string.Format("if not res.headers.get('Location').index('{0}')",Keyword));
                        break;
                    case ("Regex"):
                        if (!(Keyword.StartsWith("/") && Keyword.EndsWith("/"))) throw new Exception("Invalid Pseudo Code");
                        Py.Append("\t\t\t"); Py.AppendLine(string.Format("if re.match('{0}', res.Headers.Get('Location')):", Keyword.Trim('/')));
                        Rb.Append("\t\t\t"); Rb.AppendLine(string.Format("if res.headers.get('Location') =~ /{0}/", Keyword.Trim('/')));
                        break;
                }
                Py.Append("\t\t\t\t"); Py.AppendLine("try:");
                Py.Append("\t\t\t\t\t"); Py.AppendLine("redirect_req = req.GetRedirect(res)");
                Py.Append("\t\t\t\t\t"); Py.AppendLine("if redirect_req:");
                Py.Append("\t\t\t\t\t\t"); Py.AppendLine("res = redirect_req.Send()");
                Py.Append("\t\t\t\t\t\t"); Py.AppendLine("self.Trace(redirect_req, 'Followed redirection in the response for Main Request', 'The response for the Main Request had a redirect that matched the signature specified by user, so it was followed. Click on the load request/response button to view the request that was made to follow the redirect and its response.')");
                Py.Append("\t\t\t\t\t\t"); Py.AppendLine("return res");
                Py.Append("\t\t\t\t\t"); Py.AppendLine("else:");
                Py.Append("\t\t\t\t\t\t"); Py.AppendLine("self.Trace(req, 'No redirect in the in the response for Main Request', 'The response for the Main Request did not have a redirect so the follow redirect action is not being performed. Click on the load request/response button to view the Main Request and its response.')");
                Py.Append("\t\t\t\t\t\t"); Py.AppendLine("return res");
                Py.Append("\t\t\t\t"); Py.AppendLine("except Exception as e:");
                Py.Append("\t\t\t\t\t"); Py.AppendLine("self.Trace(req, 'Error following redirection in the response for Main Request', 'The response for the Main Request had a redirect that matched the signature specified by user, so it was followed. But there was an error when following it. Click on the load request/response button to view the Main Request and its response. Exception details - ' + e.Message)");
                Py.Append("\t\t\t\t\t"); Py.AppendLine("raise e");

                Rb.Append("\t\t\t\t"); Rb.AppendLine("begin");
                Rb.Append("\t\t\t\t\t"); Rb.AppendLine("redirect_req = req.get_redirect(res)");
                Rb.Append("\t\t\t\t\t"); Rb.AppendLine("if redirect_req");
                Rb.Append("\t\t\t\t\t\t"); Rb.AppendLine("res = redirect_req.send_req");
                Rb.Append("\t\t\t\t\t\t"); Rb.AppendLine("Trace(redirect_req, 'Followed redirection in the response for Main Request', 'The response for the Main Request had a redirect that matched the signature specified by user, so it was followed. Click on the load request/response button to view the request that was made to follow the redirect and its response.')");
                Rb.Append("\t\t\t\t\t\t"); Rb.AppendLine("return res");
                Rb.Append("\t\t\t\t\t"); Rb.AppendLine("else");
                Rb.Append("\t\t\t\t\t\t"); Rb.AppendLine("Trace(req, 'No redirect in the in the response for Main Request', 'The response for the Main Request did not have a redirect so the follow redirect action is not being performed.  Click on the load request/response button to view the Main Request and its response.')");
                Rb.Append("\t\t\t\t\t\t"); Rb.AppendLine("return res");
                Rb.Append("\t\t\t\t\t"); Rb.AppendLine("end");
                Rb.Append("\t\t\t\t"); Rb.AppendLine("rescue => e");
                Rb.Append("\t\t\t\t\t"); Rb.AppendLine("Trace(req, 'Error following redirection in the response for Main Request', 'The response for the Main Request had a redirect that matched the signature specified by user, so it was followed. But there was an error when following it.  Click on the load request/response button to view the Main Request and its response. Exception details - ' + e.Message)");
                Rb.Append("\t\t\t\t\t"); Rb.AppendLine("raise e");
                Rb.Append("\t\t\t\t"); Rb.AppendLine("end");
            }
            Py.Append("\t\t\t"); Py.AppendLine("else:");
            Py.Append("\t\t\t\t"); Py.AppendLine("self.Trace(req, 'Did not follow redirection in the response for Main Request', 'The response for the Main Request did not match the redirect signature specified by user so it was not followed. Click on the load request/response button to view the Main Request and its response.')");
            Py.Append("\t\t\t\t"); Py.AppendLine("return res");
            Py.Append("\t\t"); Py.AppendLine("self.Trace(req, 'Did not follow redirection in the response for Main Request', 'The response for the Main Request did not have a Location header. Click on the load request/response button to view the Main Request and its response.')");
            Py.Append("\t\t"); Py.AppendLine("return res");
            Py.AppendLine();

            Rb.Append("\t\t\t"); Rb.AppendLine("else");
            Rb.Append("\t\t\t\t"); Rb.AppendLine("Trace(req, 'Did not follow redirection in the response for Main Request', 'The response for the Main Request did not match the redirect signature specified by user so it was not followed. Click on the load request/response button to view the Main Request and its response.')");
            Rb.Append("\t\t\t\t"); Rb.AppendLine("return res");
            Rb.Append("\t\t\t"); Rb.AppendLine("end");
            Rb.Append("\t\t"); Rb.AppendLine("end");
            Rb.Append("\t\t"); Rb.AppendLine("Trace(req, 'Did not follow redirection in the response for Main Request', 'The response for the Main Request did not have a Location header. Click on the load request/response button to view the Main Request and its response.')");
            Rb.Append("\t\t"); Rb.AppendLine("return res");
            Rb.Append("\t"); Rb.AppendLine("end");
            Rb.AppendLine();

            Code[0] = Py.ToString();
            Code[1] = Rb.ToString();
            return Code;
        }
        string[] LoginActionPseudoCodeToCode()
        {
            RequestDeclarationsInCode[LoginCheckLabel] = new Dictionary<string, string>();
            RequestDeclarationsInCode[PerformLoginLabel] = new Dictionary<string, string>();

            string[] Code = new string[2];
            StringBuilder Py = new StringBuilder();
            StringBuilder Rb = new StringBuilder();
            string[] Steps = LoginActionPseudoCode.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            List<string> LoginCheckRequestSteps = new List<string>();
            List<string> LoggedInLoggedOutSignatureSteps = new List<string>();
            List<string> PerformLoginSteps = new List<string>();

            string Mode = "LoginCheck";
            for (int i = 0; i < Steps.Length; i++)
            {
                switch (Mode)
                {
                    case("LoginCheck"):
                        if (Steps[i].Equals("LoggedInSignatureBegins") || Steps[i].Equals("LoggedOutSignatureBegins"))
                        {
                            Mode = "LoggedInLoggedOutSignature";
                            LoggedInLoggedOutSignatureSteps.Add(Steps[i]);
                        }
                        else
                        {
                            LoginCheckRequestSteps.Add(Steps[i]);
                        }
                        break;
                    case("LoggedInLoggedOutSignature"):
                        if (Steps[i].Equals("LoggedInSignatureEnds") || Steps[i].Equals("LoggedOutSignatureEnds"))
                        {
                            Mode = "PerformLogin";
                        }
                        LoggedInLoggedOutSignatureSteps.Add(Steps[i]);
                        break;
                    case ("PerformLogin"):
                        PerformLoginSteps.Add(Steps[i]);
                        break;
                }
            }

            Py.AppendLine();
            Py.Append("\t"); Py.AppendLine("def is_logged_in(self):");

            Rb.AppendLine();
            Rb.Append("\t"); Rb.AppendLine("def is_logged_in()");

            string[] LoginCheckCode = RequestSourceAndParametersUpdatePseudoCodeToCode(LoginCheckLabel, LoginCheckRequestSteps);
            Py.Append(LoginCheckCode[0]);
            Rb.Append(LoginCheckCode[1]);

            string[] ResponseSignatureCode = ResponseSignaturePseudoCodeToCode(LoginCheckLabel, LoggedInLoggedOutSignatureSteps, string.Format("res_{0}",LoginCheckCode[3]));
            Py.Append(ResponseSignatureCode[0]);
            Py.AppendLine();
            Rb.Append(ResponseSignatureCode[1]);
            Rb.Append("\t"); Rb.AppendLine("end");
            Rb.AppendLine();
            
            Py.AppendLine();
            Py.Append("\t"); Py.AppendLine("def login_user(self, req):");
            Py.Append("\t\t"); Py.AppendLine("self.Trace(None, 'User is logged out, performing login', 'User is logged out, performing login')");

            Rb.AppendLine();
            Rb.Append("\t"); Rb.AppendLine("def login_user(req)");
            Rb.Append("\t\t"); Rb.AppendLine("Trace(nil, 'User is logged out, performing login', 'User is logged out, performing login')");

            string[] PerformLoginCode = RequestSourceAndParametersUpdatePseudoCodeToCode(PerformLoginLabel, PerformLoginSteps);
            Py.Append(PerformLoginCode[0]);
            Rb.Append(PerformLoginCode[1]);

            Py.Append("\t\t"); Py.AppendLine("if self.is_logged_in():");
            Py.Append("\t\t\t"); Py.AppendLine("self.Trace(None, 'Login Successful', 'User was successfully logged in.')");
            Py.Append("\t\t"); Py.AppendLine("else:");
            Py.Append("\t\t\t"); Py.AppendLine("self.Trace(None, 'Login failed', 'Unable to perform a successful login')");
            Py.Append("\t\t\t"); Py.AppendLine("raise Exception('Unable to log user in')");
            Py.AppendLine();

            Rb.Append("\t\t"); Rb.AppendLine("if is_logged_in");
            Rb.Append("\t\t\t"); Rb.AppendLine("Trace(nil, 'Login Successful', 'User was successfully logged in.')");
            Rb.Append("\t\t"); Rb.AppendLine("else");
            Rb.Append("\t\t\t"); Rb.AppendLine("Trace(nil, 'Login failed', 'Unable to perform a successful login')");
            Rb.Append("\t\t\t"); Rb.AppendLine("raise 'Unable to log user in'");
            Rb.Append("\t\t"); Rb.AppendLine("end");
            Rb.AppendLine();

            Rb.Append("\t"); Rb.AppendLine("end");

            Code[0] = Py.ToString();
            Code[1] = Rb.ToString();
            return Code;
        }
        string[] MultiStepActionPseudoCodeToCode()
        {
            RequestDeclarationsInCode[MultiStepPreLabel] = new Dictionary<string, string>();
            RequestDeclarationsInCode[MultiStepPostLabel] = new Dictionary<string, string>();

            string[] Code = new string[2];
            StringBuilder Py = new StringBuilder();
            StringBuilder Rb = new StringBuilder();
            string[] Steps = MultiStepActionPseudoCode.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            List<string> PreInjectionSteps = new List<string>();
            List<string> PostInjectionSteps = new List<string>();

            string Mode = "-";
            for (int i = 0; i < Steps.Length; i++)
            {
                switch (Mode)
                {
                    case ("-"):
                        if (Steps[i].Equals("PreInjectionCode:"))
                            Mode = "PreInjection";
                        else if (Steps[i].Equals("PostInjectionCode:"))
                            Mode = "PostInjection";
                        else
                            throw new Exception("Invalid Pseudo Code");
                        break;
                    case ("PreInjection"):
                        if (Steps[i].Equals("PostInjectionCode:"))
                            Mode = "PostInjection";
                        else
                            PreInjectionSteps.Add(Steps[i]);
                        break;
                    case ("PostInjection"):
                        PostInjectionSteps.Add(Steps[i]);
                        break;
                }
            }
            Py.AppendLine();
            Py.Append("\t"); Py.AppendLine("def multi_step_before_sending(self, req):");
            Py.Append("\t\t"); Py.AppendLine("self.Trace(None, 'Going to perform pre-\\'Main Request\\' steps of Multi-Step form submission', 'User selected requests that must be sent as part of multi-step form submission before the \\'Main Request\\' is sent, are going to be sent now.')");

            Rb.AppendLine();
            Rb.Append("\t"); Rb.AppendLine("def multi_step_before_sending(req)");
            Rb.Append("\t\t"); Rb.AppendLine("Trace(nil, 'Going to perform pre-\\'Main Request\\' steps of Multi-Step form submission', 'User selected requests that must be sent as part of multi-step form submission before the \\'Main Request\\' is sent, are going to be sent now.')");

            string[] PreInjectionCode = RequestSourceAndParametersUpdatePseudoCodeToCode(MultiStepPreLabel, PreInjectionSteps);
            Py.Append(PreInjectionCode[0]);
            Rb.Append(PreInjectionCode[1]);

            Py.Append("\t\t"); Py.AppendLine("return req");
            Py.AppendLine();

            Rb.Append("\t\t"); Rb.AppendLine("return req");
            Rb.Append("\t"); Rb.AppendLine("end");
            Rb.AppendLine();

            Py.AppendLine();
            Py.Append("\t"); Py.AppendLine("def multi_step_after_sending(self, res, req):");
            Py.Append("\t\t"); Py.AppendLine("self.Trace(None, 'Going to perform post-\\'Main Request\\' steps of Multi-Step form submission', 'User selected requests that must be sent as part of multi-step form submission after the \\'Main Request\\' is sent, are going to be sent now.')");

            Rb.AppendLine();
            Rb.Append("\t"); Rb.AppendLine("def multi_step_after_sending(res, req)");
            Rb.Append("\t\t"); Rb.AppendLine("Trace(nil, 'Going to perform post-\\'Main Request\\' steps of Multi-Step form submission', 'User selected requests that must be sent as part of multi-step form submission after the \\'Main Request\\' is sent, are going to be sent now.')");

            string[] PostInjectionCode = RequestSourceAndParametersUpdatePseudoCodeToCode(MultiStepPostLabel, PostInjectionSteps);
            Py.Append(PostInjectionCode[0]);
            Rb.Append(PostInjectionCode[1]);
            if (PostInjectionCode[3].Equals("0"))
            {
                Py.Append("\t\t"); Py.AppendLine("return res");
                Rb.Append("\t\t"); Rb.AppendLine("return res");
            }
            else
            {
                Py.Append("\t\t"); Py.AppendLine(string.Format("return res_{0}", PostInjectionCode[3]));
                Rb.Append("\t\t"); Rb.AppendLine(string.Format("return res_{0}", PostInjectionCode[3]));
            }
            Py.AppendLine();
            Rb.Append("\t"); Rb.AppendLine("end");
            Rb.AppendLine();

            Code[0] = Py.ToString();
            Code[1] = Rb.ToString();
            return Code;
        }
        string[] UpdateCSRFTokensPseudoCodeToCode()
        {
            RequestDeclarationsInCode[UpdateCsrfLabel] = new Dictionary<string, string>();
            
            string[] Code = new string[2];
            StringBuilder Py = new StringBuilder();
            StringBuilder Rb = new StringBuilder();
            List<string> TokenUpdateSteps = new List<string>(UpdateCSRFTokensPseudoCode.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));

            Py.AppendLine();
            Py.Append("\t"); Py.AppendLine("def update_csrf_tokens(self, req):");
            Py.Append("\t\t"); Py.AppendLine("self.Trace(None, 'Going to update CSRF tokens of the \\'Main Request\\'', 'Any CSRF token in the \\'Main Request\\' that have been selected to be updated before the request is sent are going to be updated now.')");

            Rb.AppendLine();
            Rb.Append("\t"); Rb.AppendLine("def update_csrf_tokens(req)");
            Rb.Append("\t\t"); Rb.AppendLine("Trace(nil, 'Going to update CSRF tokens of the \\'Main Request\\'', 'Any CSRF token in the \\'Main Request\\' that have been selected to be updated before the request is sent are going to be updated now.')");

            string[] CSRFTokensUpdateCode = RequestSourceAndParametersUpdatePseudoCodeToCode(UpdateCsrfLabel, TokenUpdateSteps);
            Py.Append(CSRFTokensUpdateCode[0]);
            Rb.Append(CSRFTokensUpdateCode[1]);

            Py.Append("\t\t"); Py.AppendLine("return req");
            Py.AppendLine();

            Rb.Append("\t\t"); Rb.AppendLine("return req");
            Rb.Append("\t"); Rb.AppendLine("end");
            Rb.AppendLine();

            Code[0] = Py.ToString();
            Code[1] = Rb.ToString();
            return Code;
        }
        
        string[] RequestSourceAndParametersUpdatePseudoCodeToCode(string Label, List<string> PseudoCodeLines)
        {
            return RequestSourceAndParametersUpdatePseudoCodeToCode(Label, PseudoCodeLines, 0, 0, 0);
        }
        string[] RequestSourceAndParametersUpdatePseudoCodeToCode(string Label, List<string> PseudoCodeLines, int ReqCounter, int ResCounter, int ParamCounter)
        {
            string[] Code = new string[4];
            StringBuilder Py = new StringBuilder();
            StringBuilder Rb = new StringBuilder();
            int MaxCounter = 0;
            RequestDeclarationsInCode[Label] = new Dictionary<string, string>();

            Regex RequestSourceRegex = new Regex(@"GetRequest\[(.*?)\] (.+?)\-(\d+)");

            List<string> RequestNames = new List<string>();

            foreach (string PseudoCodeLine in PseudoCodeLines)
            {
                if (PseudoCodeLine.StartsWith("GetRequest"))
                {
                    Match M = RequestSourceRegex.Match(PseudoCodeLine);
                    if (M.Success)
                    {
                        string RequestName = M.Groups[1].Value;
                        RequestNames.Add(RequestName);
                        string Source = M.Groups[2].Value;
                        string ID = M.Groups[3].Value;
                        string RequestBinaryString = "";
                        string RequestDeclarationName = "";
                        string RequestDeclarationValue = "";
                        ReqCounter++;

                        switch (Source)
                        {
                            case ("Proxy"):
                                RequestBinaryString = Request.FromProxyLog(Int32.Parse(ID)).ToBinaryString();
                                break;
                            case ("Probe"):
                                RequestBinaryString = Request.FromProbeLog(Int32.Parse(ID)).ToBinaryString();
                                break;
                            case ("Test"):
                                RequestBinaryString = Request.FromTestLog(Int32.Parse(ID)).ToBinaryString();
                                break;
                            case ("Shell"):
                                RequestBinaryString = Request.FromShellLog(Int32.Parse(ID)).ToBinaryString();
                                break;
                            case ("Scan"):
                                RequestBinaryString = Request.FromScanLog(Int32.Parse(ID)).ToBinaryString();
                                break;
                            default:
                                RequestBinaryString = Request.FromLog(Int32.Parse(ID), Source).ToBinaryString();
                                break;
                        }
                        RequestDeclarationName = string.Format("{0}_req_{1}", Label, ReqCounter);
                        RequestDeclarationValue = string.Format("Request.FromBinaryString('{0}')#{1}-{2}", RequestBinaryString, Source, ID);
                        RequestDeclarationsInCode[Label][RequestDeclarationName] = RequestDeclarationValue;

                        Py.Append("\t\t"); Py.AppendLine(string.Format("#Getting request with name '{0}' from the stored values", RequestName));
                        Py.Append("\t\t"); Py.AppendLine(string.Format("req_{0} = self.reqs['{1}_req_{0}'].GetClone()", ReqCounter, Label));
                        Py.Append("\t\t"); Py.AppendLine(string.Format("req_{0}.SetSource(self.Name + 'SP')", ReqCounter));
                        Py.Append("\t\t"); Py.AppendLine(string.Format("req_{0}.SetCookie(self.cookie_store)", ReqCounter));

                        Rb.Append("\t\t"); Rb.AppendLine(string.Format("#Getting request with name '{0}' from the stored values", RequestName));
                        Rb.Append("\t\t"); Rb.AppendLine(string.Format("req_{0} = @reqs['{1}_req_{0}'].get_clone", ReqCounter, Label));
                        Rb.Append("\t\t"); Rb.AppendLine(string.Format("req_{0}.set_source(name + 'SP')", ReqCounter));
                        Rb.Append("\t\t"); Rb.AppendLine(string.Format("req_{0}.set_cookie(@cookie_store)", ReqCounter));
                    }
                    else if (PseudoCodeLine.Equals("GetRequest ToInject"))
                    {
                        MaxCounter = ReqCounter;
                        Py.Append("\t\t"); Py.AppendLine("#Update the cookies of the request from the Cookie store");
                        Py.Append("\t\t"); Py.AppendLine("req.SetCookie(self.cookie_store)");

                        Rb.Append("\t\t"); Rb.AppendLine("#Update the cookies of the request from the Cookie store");
                        Rb.Append("\t\t"); Rb.AppendLine("req.set_cookie(@cookie_store)");
                    }
                    else
                    {
                        throw new Exception("Invalid Pseudo Code");
                    }
                }
                else if (PseudoCodeLine.Equals("SendRequest"))
                {
                    ResCounter++;
                    Py.Append("\t\t"); Py.AppendLine(string.Format("res_{0} = None", ResCounter));
                    Py.Append("\t\t"); Py.AppendLine("try:");
                    Py.Append("\t\t\t"); Py.AppendLine(string.Format("res_{0} = req_{1}.Send()", ResCounter, ReqCounter));
                    Py.Append("\t\t\t"); Py.AppendLine(string.Format("self.Trace(req_{0}, 'Sent \\'{1}\\'', 'The \\'{2}\\' request has been successfully sent. Click on the load request/response button to view this request and its response.')", ReqCounter, RequestNames[RequestNames.Count - 1], RequestNames[RequestNames.Count - 1]));
                    Py.Append("\t\t"); Py.AppendLine("except Exception as e:");
                    Py.Append("\t\t\t"); Py.AppendLine(string.Format("self.Trace(req_{0}, 'Error sending \\'{1}\\'', e.Message)", ReqCounter, RequestNames[RequestNames.Count - 1]));
                    Py.Append("\t\t\t"); Py.AppendLine("raise e");
                    Py.Append("\t\t"); Py.AppendLine(string.Format("self.cookie_store.Add(req_{0}, res_{1})", ReqCounter, ResCounter));

                    Rb.Append("\t\t"); Rb.AppendLine(string.Format("res_{0} = nil", ResCounter));
                    Rb.Append("\t\t"); Rb.AppendLine("begin");
                    Rb.Append("\t\t\t"); Rb.AppendLine(string.Format("res_{0} = req_{1}.send_req", ResCounter, ReqCounter));
                    Rb.Append("\t\t\t"); Rb.AppendLine(string.Format("Trace(req_{0}, 'Sent \\'{1}\\'', 'The \\'{2}\\' request has been successfully sent. Click on the load request/response button to view this request and its response.')", ReqCounter, RequestNames[RequestNames.Count - 1], RequestNames[RequestNames.Count - 1]));
                    Rb.Append("\t\t"); Rb.AppendLine("rescue => e");
                    Rb.Append("\t\t\t"); Rb.AppendLine(string.Format("Trace(req_{0}, 'Error sending \\'{1}\\'', e.Message)", ReqCounter, RequestNames[RequestNames.Count - 1]));
                    Rb.Append("\t\t\t"); Rb.AppendLine("raise e");
                    Rb.Append("\t\t"); Rb.AppendLine("end");
                    Rb.Append("\t\t"); Rb.AppendLine(string.Format("@cookie_store.add(req_{0}, res_{1})", ReqCounter, ResCounter));
                }
                else if (PseudoCodeLine.StartsWith("UpdateRequest"))
                {
                    string CurrentRequestParameterName = "";
                    string CurrentResponseParameterName = "";
                    if (ReqCounter == MaxCounter)
                        CurrentRequestParameterName = "req";//request to inject
                    else
                        CurrentRequestParameterName = string.Format("req_{0}", ReqCounter);
                    if (ResCounter == 0)
                        CurrentResponseParameterName = "res";//response from injection
                    else
                        CurrentResponseParameterName = string.Format("res_{0}", ResCounter);
                    ParamCounter++;
                    string[] ParametersUpdateCode = ParametersUpdatePseudoCodeToCode(Label, PseudoCodeLine, CurrentRequestParameterName, CurrentResponseParameterName, ParamCounter, RequestNames);
                    Py.Append(ParametersUpdateCode[0]);
                    Rb.Append(ParametersUpdateCode[1]);
                }
            }
            Code[0] = Py.ToString();
            Code[1] = Rb.ToString();
            Code[2] = ReqCounter.ToString();
            Code[3] = ResCounter.ToString();
            return Code;
        }
        string[] ParametersUpdatePseudoCodeToCode(string Label, string PseudoCodeLine, string RequestParameterName, string ResponseParameterName, int ParamCounter, List<string> RequestNames)
        {
            string[] Code = new string[4];
            StringBuilder Py = new StringBuilder();
            StringBuilder Rb = new StringBuilder();

            StringBuilder PySpl = new StringBuilder();
            StringBuilder RbSpl = new StringBuilder();

            string CurrentRequestName = "";
            string PreviousRequestName = "";

            string PreviousRequestParameterName = ResponseParameterName.Replace("res", "req");

            if (ResponseParameterName.Equals("res"))
            {
                PreviousRequestName = "Main Request";
            }
            else if (RequestNames.Count > 1)
            {
                PreviousRequestName = RequestNames[RequestNames.Count - 2];
            }
            else
            {
                PreviousRequestName = "Unnamed Request";
            }
            if (RequestParameterName.Equals("req"))
            {
                CurrentRequestName = "Main Request";
                if (RequestNames.Count > 0)
                {
                    PreviousRequestName = RequestNames[RequestNames.Count - 1];
                }
                else
                {
                    PreviousRequestName = "Unnamed Request";
                }
            }
            else if (RequestNames.Count > 0)
            {
                CurrentRequestName = RequestNames[RequestNames.Count - 1];
            }
            else
            {
                CurrentRequestName = "Unnamed Request";
            }
            

            Regex ParameterUpdateRegex = new Regex(@"UpdateRequest (UrlPathPart|Query|Body|Cookie|Header) (.+?) From (LastResponse|User) (Html|Regex|Hint)(.*)");

            Match M = ParameterUpdateRegex.Match(PseudoCodeLine);
            //"UpdateRequest (UrlPathPart|Query|Body|Cookie|Header) (.+?) From (LastResponse|User) (Html|Regex|Hint)(.*)"
            if (M.Success)
            {
                
                string RequestSection = M.Groups[1].Value;
                string ParameterName = M.Groups[2].Value;
                string UpdateFrom = M.Groups[3].Value;
                string UpdateType = M.Groups[4].Value;
                string RegexValueOrHint = M.Groups[5].Value;
                if (RegexValueOrHint.Length > 1) RegexValueOrHint = RegexValueOrHint.Substring(1);

                string CurrentParameterValueVariableName = string.Format("param_value_{0}", ParamCounter);
                if (UpdateFrom.Equals("LastResponse"))
                {
                    if (UpdateType.Equals("Html"))
                    {
                        Py.Append("\t\t"); Py.AppendLine(string.Format("{0} = None", CurrentParameterValueVariableName));
                        Py.Append("\t\t"); Py.AppendLine("try:");
                        Py.Append("\t\t\t"); Py.AppendLine("#Extract the parameter value from the HTML Form fields by parameter name");
                        Py.Append("\t\t\t"); Py.AppendLine(string.Format("{0} = {1}.Html.GetValues('input', 'name', '{2}', 'value')[0]", CurrentParameterValueVariableName, ResponseParameterName, ParameterName));
                        Py.Append("\t\t\t"); Py.AppendLine(string.Format("self.Trace({0}, 'Reading parameter value from response of \\'{1}\\'', 'Read the response HTML input field with name {2} and got the value -' + {3} + '\\r\\n\\r\\nClick on the load request/response button to view this response.')", PreviousRequestParameterName, PreviousRequestName, ParameterName, CurrentParameterValueVariableName));
                        Py.Append("\t\t"); Py.AppendLine("except Exception as e:");
                        Py.Append("\t\t\t"); Py.AppendLine(string.Format("self.Trace({0}, 'Error reading parameter value from response of \\'{1}\\'', 'There are no HTML input fields in the response with the name \\'{2}\\' that have a valid value attribute.\\r\\n\\r\\nClick on the load request/response button to view this response.')", PreviousRequestParameterName, PreviousRequestName, ParameterName));
                        Py.Append("\t\t\t"); Py.AppendLine("raise e");
                        Py.Append("\t\t\t"); Py.AppendLine("pass");//this is required because self.Trace and raise lines will be removed in the update_req methods

                        Rb.Append("\t\t"); Rb.AppendLine(string.Format("{0} = nil", CurrentParameterValueVariableName));
                        Rb.Append("\t\t"); Rb.AppendLine("begin");
                        Rb.Append("\t\t\t"); Rb.AppendLine("#Extract the parameter value from the HTML Form fields by parameter name");
                        Rb.Append("\t\t\t"); Rb.AppendLine(string.Format("{0} = {1}.html.get_values('input', 'name', '{2}', 'value')[0]", CurrentParameterValueVariableName, ResponseParameterName, ParameterName));
                        Rb.Append("\t\t\t"); Rb.AppendLine(string.Format(@"Trace({0}, 'Reading parameter value from response of \'{1}\'', 'Read the response HTML input field with name {2} and got the value -' + {3} + ""\r\n\r\nClick on the load request/response button to view this response."")", PreviousRequestParameterName, PreviousRequestName, ParameterName, CurrentParameterValueVariableName));
                        Rb.Append("\t\t"); Rb.AppendLine("rescue => e");
                        Rb.Append("\t\t\t"); Rb.AppendLine(string.Format(@"Trace({0}, 'Error reading parameter value from response of \'{1}\'', 'There are no HTML input fields in the response with the name \'{2}\' that have a valid value attribute' + ""\r\n\r\nClick on the load request/response button to view this response."")", PreviousRequestParameterName, PreviousRequestName, ParameterName));
                        Rb.Append("\t\t\t"); Rb.AppendLine("raise e");
                        Rb.Append("\t\t"); Rb.AppendLine("end");
                    }
                    else if (UpdateType.Equals("Regex"))
                    {
                        RegexValueOrHint = RegexValueOrHint.Substring(1, RegexValueOrHint.Length - 2).Replace("'", "\\'");

                        Py.Append("\t\t"); Py.AppendLine(string.Format("{0} = None", CurrentParameterValueVariableName));
                        Py.Append("\t\t"); Py.AppendLine("try:");
                        Py.Append("\t\t\t"); Py.AppendLine("#Extract the parameter value from the Response body using Regex");
                        Py.Append("\t\t\t"); Py.AppendLine(string.Format("{0} = re.search('{1}', {2}.BodyString).groups()[0]", CurrentParameterValueVariableName, RegexValueOrHint, ResponseParameterName));
                        Py.Append("\t\t\t"); Py.AppendLine(string.Format("self.Trace({0}, 'Reading parameter value from response of \\'{1}\\'', 'Parsed the response with the regex \\'{2}\\' and got the value -' + {3} + '.\\r\\n Using this to update the {4} parameter.\\r\\n\\r\\nClick on the load request/response button to view this response.')", PreviousRequestParameterName, PreviousRequestName, RegexValueOrHint, CurrentParameterValueVariableName, ParameterName));
                        Py.Append("\t\t"); Py.AppendLine("except Exception as e:");
                        Py.Append("\t\t\t"); Py.AppendLine(string.Format("self.Trace({0}, 'Error reading parameter value from response of \\'{1}\\'', 'There are no matches for the regex \\'{2}\\' in the response, unable to update the {3} parameter.\\r\\n\\r\\nClick on the load request/response button to view this response.')", PreviousRequestParameterName, PreviousRequestName, RegexValueOrHint, ParameterName));
                        Py.Append("\t\t\t"); Py.AppendLine("raise e");
                        Py.Append("\t\t\t"); Py.AppendLine("pass");//this is required because self.Trace and raise lines will be removed in the update_req methods

                        Rb.Append("\t\t"); Rb.AppendLine(string.Format("{0} = nil", CurrentParameterValueVariableName));
                        Rb.Append("\t\t"); Rb.AppendLine("begin");
                        Rb.Append("\t\t\t"); Rb.AppendLine("#Extract the parameter value from the Response body using Regex");
                        Rb.Append("\t\t\t"); Rb.AppendLine(string.Format("{0} = {1}.body_string.scan(/{2}/)[0][0]", CurrentParameterValueVariableName, ResponseParameterName, RegexValueOrHint));
                        Rb.Append("\t\t\t"); Rb.AppendLine(string.Format(@"Trace({0}, 'Reading parameter value from response of \'{1}\'', 'Parsed the response with the regex \'{2}\' and got the value -' + {3} + ""\r\nUsing this to update the {4} parameter\r\n\r\nClick on the load request/response button to view this response."")", PreviousRequestParameterName, PreviousRequestName, RegexValueOrHint, CurrentParameterValueVariableName, ParameterName));
                        Rb.Append("\t\t"); Rb.AppendLine("rescue => e");
                        Rb.Append("\t\t\t"); Rb.AppendLine(string.Format(@"Trace({0}, 'Error reading parameter value from response of \'{1}\'', 'There are no matches for the regex \'{2}\' in the response, unable to update the {3} parameter' + ""\r\n\r\nClick on the load request/response button to view this response."")", PreviousRequestParameterName, PreviousRequestName, RegexValueOrHint, ParameterName));
                        Rb.Append("\t\t\t"); Rb.AppendLine("raise e");
                        Rb.Append("\t\t"); Rb.AppendLine("end");
                    }
                }
                else if (UpdateFrom.Equals("User"))
                {
                    Py.Append("\t\t"); Py.AppendLine("#Get the parameter value from the user at runtime");
                    Py.Append("\t\t"); Py.AppendLine(string.Format("self.Trace(None, 'Asking the user for parameter value', 'A prompt was thrown to the user along with a hint to enter the value of the {0} parameter')", ParameterName));
                    Py.Append("\t\t"); Py.AppendLine(string.Format("{0} = AskUser.ForString('Enter the parameter value for Session Plugin:', self.get_ask_user_message_format('{1}', '{2}', '{3}'))", CurrentParameterValueVariableName, RequestSection, ParameterName, RegexValueOrHint));
                    Py.Append("\t\t"); Py.AppendLine(string.Format("self.Trace(None, 'Got parameter value from the user', 'A prompt was thrown to the user and the user entered the value -' + {0} + '.\\r\\n Using this to update the {1} parameter.')", CurrentParameterValueVariableName, ParameterName));

                    Rb.Append("\t\t"); Rb.AppendLine("#Get the parameter value from the user at runtime");
                    Rb.Append("\t\t"); Rb.AppendLine(string.Format(@"Trace(nil, 'Asking the user for parameter value', 'A prompt was thrown to the user along with a hint to enter the value of the {0} parameter')", ParameterName));
                    Rb.Append("\t\t"); Rb.AppendLine(string.Format("{0} = AskUser.for_string('Enter the parameter value for Session Plugin:', get_ask_user_message_format('{1}', '{2}', '{3}'))", CurrentParameterValueVariableName, RequestSection, ParameterName, RegexValueOrHint));
                    Rb.Append("\t\t"); Rb.AppendLine(string.Format(@"Trace(nil, 'Got parameter value from the user', 'A prompt was thrown to the user and the user entered the value -' + {0} + ""\r\nUsing this to update the {1} parameter."")", CurrentParameterValueVariableName, ParameterName));
                }
                switch (RequestSection)
                {
                    case ("UrlPathPart"):
                        Py.Append("\t\t\t");Py.AppendLine("#Update the Url path part field with new value");
                        Py.Append("\t\t"); Py.AppendLine(string.Format("upp = {0}.UrlPathParts", RequestParameterName));
                        Py.Append("\t\t"); Py.AppendLine(string.Format("if {0} < len(upp):", ParameterName));
                        Py.Append("\t\t\t"); Py.AppendLine(string.Format("upp[{0}] = {1}", ParameterName, CurrentParameterValueVariableName));
                        Py.Append("\t\t\t"); Py.AppendLine(string.Format("{0}.UrlPathParts = upp", RequestParameterName));
                        Py.Append("\t\t"); Py.AppendLine("else:");
                        Py.Append("\t\t\t"); Py.AppendLine(string.Format("self.Trace({0}, 'Error updating {1}', 'The {2} request contains only ' + str(len(upp)) + ' UrlPathParts so could not update at position {3}. (Please note that UrlPathParts positions are zero-based so first position is 0, second is 1 and so on)\\r\\n\\r\\nThe Request is:\\r\\n\\r\\n' + {4}.ToString())", RequestParameterName, CurrentRequestName, CurrentRequestName, ParameterName, RequestParameterName));
                        Py.Append("\t\t\t"); Py.AppendLine("raise Exception('Error updating request urlpathpart parameter')");
                        Py.Append("\t\t\t"); Py.AppendLine("pass");//this is required because self.Trace and raise lines will be removed in the update_req methods
                        PySpl.Append(Py.ToString());

                        Rb.Append("\t\t");Rb.AppendLine("#Update the Url path part field with new value");
                        Rb.Append("\t\t"); Rb.AppendLine(string.Format("upp = {0}.url_path_parts", RequestParameterName));
                        Rb.Append("\t\t"); Rb.AppendLine(string.Format("if {0} < upp.count", ParameterName));
                        Rb.Append("\t\t\t"); Rb.AppendLine(string.Format("upp[{0}] = {1}", ParameterName, CurrentParameterValueVariableName));
                        Rb.Append("\t\t\t"); Rb.AppendLine(string.Format("{0}.url_path_parts = upp", RequestParameterName));
                        Rb.Append("\t\t"); Rb.AppendLine("else");
                        Rb.Append("\t\t\t"); Rb.AppendLine(string.Format(@"Trace({0}, 'Error updating {1}', 'The {2} request contains only ' + upp.count.to_s + ' UrlPathParts so could not update at position {3}. (Please note that UrlPathParts positions are zero-based so first position is 0, second is 1 and so on)' + ""\r\n\r\nThe Request is:\r\n\r\n"" + {4}.to_string)", RequestParameterName, CurrentRequestName, CurrentRequestName, ParameterName, RequestParameterName));
                        Rb.Append("\t\t\t"); Rb.AppendLine("raise 'Error updating request urlpathpart parameter'");
                        Rb.Append("\t\t"); Rb.AppendLine("end");
                        RbSpl.Append(Rb.ToString());
                        break;
                    case ("Query"):
                        //Update the query parameter of specified request 
                        Py.Append("\t\t");Py.AppendLine("#Update the Query parameter with the new value");
                        Py.Append("\t\t"); Py.AppendLine(string.Format("if {0}.Query.Has('{1}'):", RequestParameterName, ParameterName));
                        Py.Append("\t\t\t"); Py.AppendLine(string.Format("{0}.Query.Set('{1}', {2})", RequestParameterName, ParameterName, CurrentParameterValueVariableName));
                        Py.Append("\t\t"); Py.AppendLine("else:");
                        Py.Append("\t\t\t"); Py.AppendLine(string.Format("self.Trace({0}, 'Error updating {1}', 'The {2} request does not contain the query parameter {3}\\r\\n\\r\\nThe Request is:\\r\\n\\r\\n' + {4}.ToString())", RequestParameterName, CurrentRequestName, CurrentRequestName, ParameterName, RequestParameterName));
                        Py.Append("\t\t\t"); Py.AppendLine("raise Exception('Error updating request query parameter')");
                        Py.Append("\t\t\t"); Py.AppendLine("pass");//this is required because self.Trace and raise lines will be removed in the update_req methods
                        PySpl.Append(Py.ToString());

                        Rb.Append("\t\t");Rb.AppendLine("#Update the Query parameter with the new value");
                        Rb.Append("\t\t"); Rb.AppendLine(string.Format("if {0}.query.has('{1}')", RequestParameterName, ParameterName));
                        Rb.Append("\t\t\t"); Rb.AppendLine(string.Format("{0}.query.set('{1}', {2})", RequestParameterName, ParameterName, CurrentParameterValueVariableName));
                        Rb.Append("\t\t"); Rb.AppendLine("else");
                        Rb.Append("\t\t\t"); Rb.AppendLine(string.Format(@"Trace({0}, 'Error updating {1}', 'The {2} request does not contain the query parameter {3}' + ""\r\n\r\nThe Request is:\r\n\r\n"" + {4}.to_string)", RequestParameterName, CurrentRequestName, CurrentRequestName, ParameterName, RequestParameterName));
                        Rb.Append("\t\t\t"); Rb.AppendLine("raise 'Error updating request query parameter'");
                        Rb.Append("\t\t"); Rb.AppendLine("end");
                        RbSpl.Append(Rb.ToString());

                        if (RequestParameterName.Equals("req"))
                        {
                            //Update the query parameter of all requests that have this parameter
                            Py.Append("\t\t"); Py.AppendLine("#Update the Query parameter of the other requests that have the same parameter with the new value");
                            Py.Append("\t\t"); Py.AppendLine("for r_n in self.reqs.keys():");
                            Py.Append("\t\t\t"); Py.AppendLine(string.Format("if self.reqs[r_n].Query.Has('{0}'):", ParameterName));
                            Py.Append("\t\t\t\t"); Py.AppendLine(string.Format("self.reqs[r_n].Query.Set('{0}', {1})", ParameterName, CurrentParameterValueVariableName));

                            Rb.Append("\t\t"); Rb.AppendLine("#Update the Query parameter of the other requests that have the same parameter with the new value");
                            Rb.Append("\t\t"); Rb.AppendLine("for r_n in @reqs.keys");
                            Rb.Append("\t\t\t"); Rb.AppendLine(string.Format("if @reqs[r_n].query.has('{0}')", ParameterName));
                            Rb.Append("\t\t\t\t"); Rb.AppendLine(string.Format("@reqs[r_n].query.set('{0}', {1})", ParameterName, CurrentParameterValueVariableName));
                            Rb.Append("\t\t\t"); Rb.AppendLine("end");
                            Rb.Append("\t\t"); Rb.AppendLine("end");
                        }
                        break;
                    case ("Body"):
                        //Update the body parameter of specified request 
                        Py.Append("\t\t");Py.AppendLine("#Update the Body parameter with the new value");
                        Py.Append("\t\t"); Py.AppendLine(string.Format("if {0}.Body.Has('{1}'):", RequestParameterName, ParameterName));
                        Py.Append("\t\t\t"); Py.AppendLine(string.Format("{0}.Body.Set('{1}', {2})", RequestParameterName, ParameterName, CurrentParameterValueVariableName));
                        Py.Append("\t\t"); Py.AppendLine("else:");
                        Py.Append("\t\t\t"); Py.AppendLine(string.Format("self.Trace({0}, 'Error updating {1}', 'The {2} request does not contain the body parameter {3}\\r\\n\\r\\nThe Request is:\\r\\n\\r\\n' + {4}.ToString())", RequestParameterName, CurrentRequestName, CurrentRequestName, ParameterName, RequestParameterName));
                        Py.Append("\t\t\t"); Py.AppendLine("raise Exception('Error updating request body parameter')");
                        Py.Append("\t\t\t"); Py.AppendLine("pass");//this is required because self.Trace and raise lines will be removed in the update_req methods
                        PySpl.Append(Py.ToString());

                        Rb.Append("\t\t");Rb.AppendLine("#Update the Body parameter with the new value");
                        Rb.Append("\t\t"); Rb.AppendLine(string.Format("if {0}.body.has('{1}')", RequestParameterName, ParameterName));
                        Rb.Append("\t\t\t"); Rb.AppendLine(string.Format("{0}.body.set('{1}', {2})", RequestParameterName, ParameterName, CurrentParameterValueVariableName));
                        Rb.Append("\t\t"); Rb.AppendLine("else");
                        Rb.Append("\t\t\t"); Rb.AppendLine(string.Format(@"Trace({0}, 'Error updating {1}', 'The {2} request does not contain the body parameter {3}' + ""\r\n\r\nThe Request is:\r\n\r\n"" + {4}.to_string)", RequestParameterName, CurrentRequestName, CurrentRequestName, ParameterName, RequestParameterName));
                        Rb.Append("\t\t\t"); Rb.AppendLine("raise 'Error updating request body parameter'");
                        Rb.Append("\t\t"); Rb.AppendLine("end");
                        RbSpl.Append(Rb.ToString());

                        if (RequestParameterName.Equals("req"))
                        {
                            //Update the body parameter of all requests that have this parameter
                            Py.Append("\t\t"); Py.AppendLine("#Update the Body parameter of the other requests that have the same parameter with the new value");
                            Py.Append("\t\t"); Py.AppendLine("for r_n in self.reqs.keys():");
                            Py.Append("\t\t\t"); Py.AppendLine(string.Format("if self.reqs[r_n].Body.Has('{0}'):", ParameterName));
                            Py.Append("\t\t\t\t"); Py.AppendLine(string.Format("self.reqs[r_n].Body.Set('{0}', {1})", ParameterName, CurrentParameterValueVariableName));

                            Rb.Append("\t\t"); Rb.AppendLine("#Update the Body parameter of the other requests that have the same parameter with the new value");
                            Rb.Append("\t\t"); Rb.AppendLine("for r_n in @reqs.keys");
                            Rb.Append("\t\t\t"); Rb.AppendLine(string.Format("if @reqs[r_n].body.has('{0}')", ParameterName));
                            Rb.Append("\t\t\t\t"); Rb.AppendLine(string.Format("@reqs[r_n].body.set('{0}', {1})", ParameterName, CurrentParameterValueVariableName));
                            Rb.Append("\t\t\t"); Rb.AppendLine("end");
                            Rb.Append("\t\t"); Rb.AppendLine("end");

                        }
                        break;
                    case ("Cookie"):
                        //Update the cookie parameter of specified request 
                        Py.Append("\t\t");Py.AppendLine("#Update the Cookie parameter with the new value");
                        Py.Append("\t\t"); Py.AppendLine(string.Format("if {0}.Cookie.Has('{1}'):", RequestParameterName, ParameterName));
                        Py.Append("\t\t\t"); Py.AppendLine(string.Format("{0}.Cookie.Set('{1}', {2})", RequestParameterName, ParameterName, CurrentParameterValueVariableName));
                        Py.Append("\t\t"); Py.AppendLine("else:");
                        Py.Append("\t\t\t"); Py.AppendLine(string.Format("self.Trace({0}, 'Error updating {1}', 'The {2} request does not contain the cookie parameter {3}\\r\\n\\r\\nThe Request is:\\r\\n\\r\\n' + {4}.ToString())", RequestParameterName, CurrentRequestName, CurrentRequestName, ParameterName, RequestParameterName));
                        Py.Append("\t\t\t"); Py.AppendLine("raise Exception('Error updating request cookie parameter')");
                        Py.Append("\t\t\t"); Py.AppendLine("pass");//this is required because self.Trace and raise lines will be removed in the update_req methods
                        PySpl.Append(Py.ToString());

                        Rb.Append("\t\t");Rb.AppendLine("#Update the Cookie parameter with the new value");
                        Rb.Append("\t\t"); Rb.AppendLine(string.Format("if {0}.cookie.has('{1}')", RequestParameterName, ParameterName));
                        Rb.Append("\t\t\t"); Rb.AppendLine(string.Format("{0}.cookie.set('{1}', {2})", RequestParameterName, ParameterName, CurrentParameterValueVariableName));
                        Rb.Append("\t\t"); Rb.AppendLine("else");
                        Rb.Append("\t\t\t"); Rb.AppendLine(string.Format(@"Trace({0}, 'Error updating {1}', 'The {2} request does not contain the cookie parameter {3}' + ""\r\n\r\nThe Request is:\r\n\r\n"" + {4}.to_string)", RequestParameterName, CurrentRequestName, CurrentRequestName, ParameterName, RequestParameterName));
                        Rb.Append("\t\t\t"); Rb.AppendLine("raise 'Error updating request cookie parameter'");
                        Rb.Append("\t\t"); Rb.AppendLine("end");
                        RbSpl.Append(Rb.ToString());

                        if (RequestParameterName.Equals("req"))
                        {
                            //Update the cookie parameter of all requests that have this parameter
                            Py.Append("\t\t"); Py.AppendLine("#Update the Cookie parameter of the other requests that have the same parameter with the new value");
                            Py.Append("\t\t"); Py.AppendLine("for r_n in self.reqs.keys():");
                            Py.Append("\t\t\t"); Py.AppendLine(string.Format("if self.reqs[r_n].Cookie.Has('{0}'):", ParameterName));
                            Py.Append("\t\t\t\t"); Py.AppendLine(string.Format("self.reqs[r_n].Cookie.Set('{0}', {1})", ParameterName, CurrentParameterValueVariableName));

                            Rb.Append("\t\t"); Rb.AppendLine("#Update the Cookie parameter of the other requests that have the same parameter with the new value");
                            Rb.Append("\t\t"); Rb.AppendLine("for r_n in @reqs.keys");
                            Rb.Append("\t\t\t"); Rb.AppendLine(string.Format("if @reqs[r_n].cookie.has('{0}')", ParameterName));
                            Rb.Append("\t\t\t\t"); Rb.AppendLine(string.Format("@reqs[r_n].cookie.set('{0}', {1})", ParameterName, CurrentParameterValueVariableName));
                            Rb.Append("\t\t\t"); Rb.AppendLine("end");
                            Rb.Append("\t\t"); Rb.AppendLine("end");
                        }
                        break;
                    case ("Header"):
                        //Update the header parameter of specified request 
                        Py.Append("\t\t");Py.AppendLine("#Update the Header parameter with the new value");
                        Py.Append("\t\t"); Py.AppendLine(string.Format("if {0}.Headers.Has('{1}'):", RequestParameterName, ParameterName));
                        Py.Append("\t\t\t"); Py.AppendLine(string.Format("{0}.Headers.Set('{1}', {2})", RequestParameterName, ParameterName, CurrentParameterValueVariableName));
                        Py.Append("\t\t"); Py.AppendLine("else:");
                        Py.Append("\t\t\t"); Py.AppendLine(string.Format("self.Trace({0}, 'Error updating {1}', 'The {2} request does not contain the header parameter {3}\\r\\n\\r\\nThe Request is:\\r\\n\\r\\n' + {4}.ToString())", RequestParameterName, CurrentRequestName, CurrentRequestName, ParameterName, RequestParameterName));
                        Py.Append("\t\t\t"); Py.AppendLine("raise Exception('Error updating request header parameter')");
                        Py.Append("\t\t\t"); Py.AppendLine("pass");//this is required because self.Trace and raise lines will be removed in the update_req methods
                        PySpl.Append(Py.ToString());

                        Rb.Append("\t\t");Rb.AppendLine("#Update the Header parameter with the new value");
                        Rb.Append("\t\t"); Rb.AppendLine(string.Format("if {0}.headers.has('{1}')", RequestParameterName, ParameterName));
                        Rb.Append("\t\t\t"); Rb.AppendLine(string.Format("{0}.headers.set('{1}', {2})", RequestParameterName, ParameterName, CurrentParameterValueVariableName));
                        Rb.Append("\t\t"); Rb.AppendLine("else");
                        Rb.Append("\t\t\t"); Rb.AppendLine(string.Format(@"Trace({0}, 'Error updating {1}', 'The {2} request does not contain the header parameter {3}' + ""\r\n\r\nThe Request is:\r\n\r\n"" + {4}.to_string)", RequestParameterName, CurrentRequestName, CurrentRequestName, ParameterName, RequestParameterName));
                        Rb.Append("\t\t\t"); Rb.AppendLine("raise 'Error updating request header parameter'");
                        Rb.Append("\t\t"); Rb.AppendLine("end");
                        RbSpl.Append(Rb.ToString());

                        if (RequestParameterName.Equals("req"))
                        {
                            //Update the header parameter of all requests that have this parameter
                            Py.Append("\t\t"); Py.AppendLine("#Update the Header parameter of the other requests that have the same parameter with the new value");
                            Py.Append("\t\t"); Py.AppendLine("for r_n in self.reqs.keys():");
                            Py.Append("\t\t\t"); Py.AppendLine(string.Format("if self.reqs[r_n].Headers.Has('{0}'):", ParameterName));
                            Py.Append("\t\t\t\t"); Py.AppendLine(string.Format("self.reqs[r_n].Headers.Set('{0}', {1})", ParameterName, CurrentParameterValueVariableName));

                            Rb.Append("\t\t"); Rb.AppendLine("#Update the Header parameter of the other requests that have the same parameter with the new value");
                            Rb.Append("\t\t"); Rb.AppendLine("for r_n in @reqs.keys");
                            Rb.Append("\t\t\t"); Rb.AppendLine(string.Format("if @reqs[r_n].headers.has('{0}')", ParameterName));
                            Rb.Append("\t\t\t\t"); Rb.AppendLine(string.Format("@reqs[r_n].headers.set('{0}', {1})", ParameterName, CurrentParameterValueVariableName));
                            Rb.Append("\t\t\t"); Rb.AppendLine("end");
                            Rb.Append("\t\t"); Rb.AppendLine("end");
                        }
                        break;
                }
            
                if (RequestParameterName.Equals("req") && !UpdateFrom.Equals("User"))
                {               
                    UpdateToInjectRequestMethodCounter++;
                    Py.Append("\t\t"); Py.AppendLine("#Store this response to update other primary requests later");
                    Py.Append("\t\t"); Py.AppendLine(string.Format("self.ress[{0}] = {1}", UpdateToInjectRequestMethodCounter, ResponseParameterName));

                    Rb.Append("\t\t"); Rb.AppendLine("#Store this response to update other primary requests later");
                    Rb.Append("\t\t"); Rb.AppendLine(string.Format("@ress[{0}] = {1}", UpdateToInjectRequestMethodCounter, ResponseParameterName));
                
                    StringBuilder PyMethodDec = new StringBuilder();
                    StringBuilder RbMethodDec = new StringBuilder();
                
                    PyMethodDec.Append("\t"); PyMethodDec.AppendLine(string.Format("def update_req_{0}(self, req):", UpdateToInjectRequestMethodCounter));
                    PyMethodDec.Append("\t\t"); PyMethodDec.AppendLine(string.Format("if not self.ress.has_key({0}):", UpdateToInjectRequestMethodCounter));
                    PyMethodDec.Append("\t\t\t"); PyMethodDec.AppendLine("return req");
                    PyMethodDec.Append("\t\t"); PyMethodDec.AppendLine(string.Format("{0} = self.ress[{1}]", ResponseParameterName, UpdateToInjectRequestMethodCounter));

                    string RawPySpl = PySpl.ToString();
                    StringBuilder TrimmedPySpl = new StringBuilder();
                    foreach(string RawPySplLine in RawPySpl.Split(new string[]{Environment.NewLine}, StringSplitOptions.None))
                    {
                        string TrimmedLine = RawPySplLine.TrimStart();
                        //Only add lines that don't have trace or exception throwing code
                        if (!(TrimmedLine.StartsWith("self.Trace(") || TrimmedLine.StartsWith("raise ")))
                        {
                            TrimmedPySpl.AppendLine(RawPySplLine);
                        }
                    }

                    PyMethodDec.Append(TrimmedPySpl.ToString());
                    PyMethodDec.Append("\t\t"); PyMethodDec.AppendLine("return req");

                    RbMethodDec.Append("\t"); RbMethodDec.AppendLine(string.Format("def update_req_{0}(req)", UpdateToInjectRequestMethodCounter));
                    RbMethodDec.Append("\t\t"); RbMethodDec.AppendLine(string.Format("if not @ress.has_key?({0})", UpdateToInjectRequestMethodCounter));
                    RbMethodDec.Append("\t\t\t"); RbMethodDec.AppendLine("return req");
                    RbMethodDec.Append("\t\t"); RbMethodDec.AppendLine("end");
                    RbMethodDec.Append("\t\t"); RbMethodDec.AppendLine(string.Format("{0} = @ress[{1}]", ResponseParameterName, UpdateToInjectRequestMethodCounter));

                    string RawRbSpl = RbSpl.ToString();
                    StringBuilder TrimmedRbSpl = new StringBuilder();
                    foreach (string RawRbSplLine in RawRbSpl.Split(new string[] { Environment.NewLine }, StringSplitOptions.None))
                    {
                        string TrimmedLine = RawRbSplLine.TrimStart();
                        //Only add lines that don't have trace or exception throwing code
                        if (!(TrimmedLine.StartsWith("Trace(") || TrimmedLine.StartsWith("raise ")))
                        {
                            TrimmedRbSpl.AppendLine(RawRbSplLine);
                        }
                    }

                    RbMethodDec.Append(TrimmedRbSpl.ToString());
                    RbMethodDec.Append("\t\t"); RbMethodDec.AppendLine("return req");
                    RbMethodDec.Append("\t"); RbMethodDec.AppendLine("end");

                    UpdateToInectRequestMethodPyDeclarations[UpdateToInjectRequestMethodCounter] = PyMethodDec.ToString();
                    UpdateToInectRequestMethodRbDeclarations[UpdateToInjectRequestMethodCounter] = RbMethodDec.ToString();
                }
                Code[0] = Py.ToString();
                Code[1] = Rb.ToString();
                return Code;
            }
            else
            {
                throw new Exception("Invalid Pseudo Code");
            }
        }
        string[] ResponseSignaturePseudoCodeToCode(string Label, List<string> PseudoCodeLines, string ResponseVariableName)
        {
            string[] Code = new string[2];
            StringBuilder Py = new StringBuilder();
            StringBuilder Rb = new StringBuilder();
            bool LoggedInSignature = false;
            if (PseudoCodeLines[0].Equals("LoggedInSignatureBegins"))
                LoggedInSignature = true;
            else
                LoggedInSignature = false;
            bool ResponseCodeChecked = false;

            Regex ResponseSignatureRegex = new Regex(@"LastResponse Code (\d{3}) (Location|Title|Body) (StartsWith|EndsWith|Has|NotHas|Regex) (.+)");

            for (int i=1; i<PseudoCodeLines.Count -1; i++)
            {
                string PseudoCodeLine = PseudoCodeLines[i];
                Match M = ResponseSignatureRegex.Match(PseudoCodeLine);
                if (M.Success)
                {
                    string ResponseCode = M.Groups[1].Value;
                    string ResponsePartToMatch = M.Groups[2].Value;
                    string MatchType = M.Groups[3].Value;
                    string KeywordOrRegex = M.Groups[4].Value;
                    if (!ResponseCodeChecked)
                    {
                        Py.Append("\t\t"); Py.AppendLine(string.Format("if {0}.Code != {1}:", ResponseVariableName, ResponseCode));
                        Rb.Append("\t\t"); Rb.AppendLine(string.Format("if {0}.code != {1}", ResponseVariableName, ResponseCode));

                        if (LoggedInSignature)
                        {
                            Py.Append("\t\t\t"); Py.AppendLine("return False");
                            Rb.Append("\t\t\t"); Rb.AppendLine("return false");
                        }
                        else
                        {
                            Py.Append("\t\t\t"); Py.AppendLine("return True");
                            Rb.Append("\t\t\t"); Rb.AppendLine("return true");
                        }
                        Rb.Append("\t\t"); Rb.AppendLine("end");
                        ResponseCodeChecked = true;
                    }
                    string PropertyValuePyCode = "";
                    string PropertyValueRbCode = "";
                    switch (ResponsePartToMatch)
                    {
                        case ("Location"):
                            PropertyValuePyCode = string.Format("{0}.Headers.Get('Location')", ResponseVariableName);
                            PropertyValueRbCode = string.Format("{0}.headers.get('Location')", ResponseVariableName);
                            break;
                        case ("Title"):
                            PropertyValuePyCode = string.Format("{0}.Html.Title", ResponseVariableName);
                            PropertyValueRbCode = string.Format("{0}.html.title", ResponseVariableName);
                            break;
                        case ("Body"):
                            PropertyValuePyCode = string.Format("{0}.BodyString", ResponseVariableName);
                            PropertyValueRbCode = string.Format("{0}.body_string", ResponseVariableName);
                            break;
                    }
                    string PropertyValueMatchPyCode = "";
                    string PropertyValueMatchRbCode = "";
                    switch (MatchType)
                    {
                        case("StartsWith"):
                            PropertyValueMatchPyCode = string.Format("{0}.startswith('{1}'):", PropertyValuePyCode, KeywordOrRegex);
                            PropertyValueMatchRbCode = string.Format("{0}.start_with?('{1}'):", PropertyValueRbCode, KeywordOrRegex);
                            break;
                        case ("EndsWith"):
                            PropertyValueMatchPyCode = string.Format("{0}.endswith('{1}'):", PropertyValuePyCode, KeywordOrRegex);
                            PropertyValueMatchRbCode = string.Format("{0}.end_with?('{1}')", PropertyValueRbCode, KeywordOrRegex);
                            break;
                        case ("Has"):
                            PropertyValueMatchPyCode = string.Format("{0}.count('{1}') > 0:", PropertyValuePyCode, KeywordOrRegex);
                            PropertyValueMatchRbCode = string.Format("{0}.index('{1}')", PropertyValueRbCode, KeywordOrRegex);
                            break;
                        case ("NotHas"):
                            PropertyValueMatchPyCode = string.Format("{0}.count('{1}') == 0:", PropertyValuePyCode, KeywordOrRegex);
                            PropertyValueMatchRbCode = string.Format("({0}.index('{1}') == nil)", PropertyValueRbCode, KeywordOrRegex);
                            break;
                        case ("Regex"):
                            KeywordOrRegex = KeywordOrRegex.Substring(1, KeywordOrRegex.Length - 2).Replace("'", "\\'");
                            PropertyValueMatchPyCode = string.Format("re.match('{0}', {1}):", KeywordOrRegex, PropertyValuePyCode);
                            PropertyValueMatchRbCode = string.Format("({0} =~ /{1}/)",PropertyValueRbCode, KeywordOrRegex);
                            break;
                    }
                    Py.Append("\t\t"); Py.AppendLine(string.Format("if not {0}", PropertyValueMatchPyCode));
                    Rb.Append("\t\t"); Rb.AppendLine(string.Format("if not {0}", PropertyValueMatchRbCode));

                    if (LoggedInSignature)
                    {
                        Py.Append("\t\t\t"); Py.AppendLine("return False");
                        Rb.Append("\t\t\t"); Rb.AppendLine("return false");
                    }
                    else
                    {
                        Py.Append("\t\t\t"); Py.AppendLine("return True");
                        Rb.Append("\t\t\t"); Rb.AppendLine("return true");
                    }
                    Rb.Append("\t\t"); Rb.AppendLine("end");
                }
                else
                {
                    throw new Exception("Invalid Pseudo Code");
                }
            }
            
            if (LoggedInSignature)
            {
                Py.Append("\t\t"); Py.AppendLine("return True");
                Rb.Append("\t\t"); Rb.AppendLine("return true");
            }
            else
            {
                Py.Append("\t\t"); Py.AppendLine("return False");
                Rb.Append("\t\t"); Rb.AppendLine("return false");
            }
            Code[0] = Py.ToString();
            Code[1] = Rb.ToString();
            return Code;
        }
        #endregion


        #region EventHandlers

        private void AnswerTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Return)
            {
                e.Handled = true;
                ProcessUserAnswer();
                AnswerTB.Text = "";
            }
        }

        private void SubmitAnswerBtn_Click(object sender, EventArgs e)
        {
            ProcessUserAnswer();
            AnswerTB.Text = "";
            BigAnswerTB.Text = "";
        }

        private void ScanCustomizationAssistant_Load(object sender, EventArgs e)
        {
            GoToHomeMenu();
        }

        private void ParametersDescLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string ParameterName = ParameterNameTB.Text.Trim();
            if (ParameterName.Length == 0)
            {
                ShowError("Parameter names cannot be empty");
                ParameterNameTB.BackColor = Color.Red;
                return;
            }
            if (ParameterTypeCombo.SelectedIndex == 0)
            {
                try
                {
                    Int32.Parse(ParameterName);
                }
                catch
                {
                    ShowError("UrlPathPart parameter index must be an integer");
                    ParameterNameTB.BackColor = Color.Red;
                    return;
                }
            }

            HowToUpdateParameterPanel.Visible = true;
            if (ParametersAnswerAskUserOnly)
            {
                ParameterSourceFromResponseRB.Checked = false;
                ParameterSourceFromResponseRB.Enabled = false;
                ParameterSourceFromUserRB.Checked = true;
                HowToParseResponsePanel.Visible = false;
                UserHintPanel.Visible = true;
            }
            else
            {
                ParameterSourceFromResponseRB.Enabled = true;
                ParameterSourceFromResponseRB.Checked = false;
                ParameterSourceFromUserRB.Checked = false;
            }
        }

        private void ParameterSourceFromResponseRB_CheckedChanged(object sender, EventArgs e)
        {
            ParameterParseRegexTB.Text = "";
            if (ParameterSourceFromResponseRB.Checked)
            {
                HowToParseResponsePanel.Visible = true;
                ParseParameterFromHtmlRB.Checked = true;
            }
            else
            {
                HowToParseResponsePanel.Visible = false;
            }
            AddParameterAnswerEntryLL.Visible = true;
        }

        private void ParameterSourceFromUserRB_CheckedChanged(object sender, EventArgs e)
        {
            if (ParameterSourceFromUserRB.Checked)
            {
                UserHintPanel.Visible = true;
            }
            else
            {
                UserHintPanel.Visible = false;
            }
            AddParameterAnswerEntryLL.Visible = true;
        }

        private void ParseParameterFromRegexRB_CheckedChanged(object sender, EventArgs e)
        {
            if (ParseParameterFromRegexRB.Checked)
                ParameterParseRegexTB.Enabled = true;
            else
                ParameterParseRegexTB.Enabled = false;
        }

        private void ParameterTypeCombo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Enter)
                e.Handled = true;
        }

        private void RequestSourceCombo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Enter)
                e.Handled = true;
        }

        private void UseLocationSignatureCB_CheckedChanged(object sender, EventArgs e)
        {
            if (UseLocationSignatureCB.Checked)
            {
                LocationSignatureTypeSelectCombo.Enabled = true;
                LocationSignatureKeywordTB.Enabled = true;
            }
            else
            {
                LocationSignatureTypeSelectCombo.Enabled = false;
                LocationSignatureKeywordTB.Enabled = false;
            }
        }

        private void UseTitleSignatureCB_CheckedChanged(object sender, EventArgs e)
        {
            if (UseTitleSignatureCB.Checked)
            {
                TitleSignatureTypeSelectCombo.Enabled = true;
                TitleSignatureKeywordTB.Enabled = true;
            }
            else
            {
                TitleSignatureTypeSelectCombo.Enabled = false;
                TitleSignatureKeywordTB.Enabled = false;
            }
        }

        private void UseBodySignatureCB_CheckedChanged(object sender, EventArgs e)
        {
            if (UseBodySignatureCB.Checked)
            {
                BodySignatureTypeSelectCombo.Enabled = true;
                BodySignatureKeywordTB.Enabled = true;
            }
            else
            {
                BodySignatureTypeSelectCombo.Enabled = false;
                BodySignatureKeywordTB.Enabled = false;
            }
        }

        private void LocationSignatureTypeSelectCombo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Enter)
                e.Handled = true;
        }

        private void TitleSignatureTypeSelectCombo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Enter)
                e.Handled = true;
        }

        private void BodySignatureTypeSelectCombo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Enter)
                e.Handled = true;
        }

        private void SignatureSubmitBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Int32.Parse(SignatureResponseCodeTB.Text);
            }
            catch
            {
                ShowError("Invalid Response Code specified");
                SignatureResponseCodeTB.BackColor = Color.Red;
                return;
            }
            if (FullResponseSignatureFirstPanel.Visible && !LoggedInResponseSignatureRB.Checked && !LoggedOutResponseSignatureRB.Checked)
            {
                ShowError("Select either Logged In Signature or Logged Out Signature");
                return;
            }
            if (UseLocationSignatureCB.Checked)
            {
                if (LocationSignatureKeywordTB.Text.Length == 0)
                {
                    ShowError("Keyword field cannot be empty");
                    LocationSignatureKeywordTB.BackColor = Color.Red;
                    return;
                }
            }
            if (UseTitleSignatureCB.Checked)
            {
                if (TitleSignatureKeywordTB.Text.Length == 0)
                {
                    ShowError("Keyword field cannot be empty");
                    TitleSignatureKeywordTB.BackColor = Color.Red;
                    return;
                }
            }
            if (UseBodySignatureCB.Checked)
            {
                if (BodySignatureKeywordTB.Text.Length == 0)
                {
                    ShowError("Keyword field cannot be empty");
                    BodySignatureKeywordTB.BackColor = Color.Red;
                    return;
                }
            }
            if (FullResponseSignatureFirstPanel.Visible)
            {
                if (!(UseLocationSignatureCB.Checked || UseTitleSignatureCB.Checked || UseBodySignatureCB.Checked))
                {
                    ShowError("Select either Location, Title or Body signature along with Response Code");
                    return;
                }
            }
            ProcessUserAnswer();
        }

        private void SignatureResponseCodeTB_TextChanged(object sender, EventArgs e)
        {
            if (SignatureResponseCodeTB.BackColor != Color.White)
            {
                SignatureResponseCodeTB.BackColor = Color.White;
            }
        }

        private void LocationSignatureKeywordTB_TextChanged(object sender, EventArgs e)
        {
            if (SignatureResponseCodeTB.BackColor != Color.White)
            {
                SignatureResponseCodeTB.BackColor = Color.White;
            }
        }

        private void TitleSignatureKeywordTB_TextChanged(object sender, EventArgs e)
        {
            if (TitleSignatureKeywordTB.BackColor != Color.White)
            {
                TitleSignatureKeywordTB.BackColor = Color.White;
            }
        }

        private void BodySignatureKeywordTB_TextChanged(object sender, EventArgs e)
        {
            if (BodySignatureKeywordTB.BackColor != Color.White)
            {
                BodySignatureKeywordTB.BackColor = Color.White;
            }
        }

        private void AnswerTabs_Selecting(object sender, TabControlCancelEventArgs e)
        {
            switch (CurrentQuestionType)
            {
                case (QuestionTypeTextAnswer):
                    if (!e.TabPage.Name.Equals("TextAnswerTab")) AnswerTabs.SelectTab("TextAnswerTab");
                    break;
                case (QuestionTypeParametersAnswer):
                    if (!e.TabPage.Name.Equals("ParameterAnswerTab")) AnswerTabs.SelectTab("ParameterAnswerTab");
                    break;
                case (QuestionTypeRequestSourceAnswer):
                    if (!e.TabPage.Name.Equals("RequestSourceAnswerTab")) AnswerTabs.SelectTab("RequestSourceAnswerTab");
                    break;
                case (QuestionTypeResponseSignatureAnswer):
                    if (!e.TabPage.Name.Equals("ResponseSignatureTab")) AnswerTabs.SelectTab("ResponseSignatureTab");
                    break;
                case (QuestionTypeSelectOptionAnswer):
                    if (!e.TabPage.Name.Equals("SelectOptionTab")) AnswerTabs.SelectTab("SelectOptionTab");
                    break;
                case (QuestionTypeShowPseudoCode):
                    if (!e.TabPage.Name.Equals("ShowPseudoCodeTab")) AnswerTabs.SelectTab("ShowPseudoCodeTab");
                    break;
            }
        }

        private void RequestSourceIdTB_TextChanged(object sender, EventArgs e)
        {
            if (RequestSourceIdTB.BackColor != Color.White)
            {
                RequestSourceIdTB.BackColor = Color.White;
            }
        }

        private void RequestSourceAnswerSubmitBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string RequestName = RequestSourceNameTB.Text.Trim();
                if (RequestName.Length == 0) throw new Exception("Request name cannot be empty");
                if (!Regex.IsMatch(RequestName, @"^[a-zA-Z0-9\s]*$")) throw new Exception("Request name can only have letters numbers and space");
            }
            catch(Exception Exp)
            {
                ShowError(Exp.Message);
                RequestSourceNameTB.BackColor = Color.Red;
                return;
            }
            try
            {
                int LogId = Int32.Parse(RequestSourceIdTB.Text);
                if (LogId < 1)
                {
                    throw new Exception("Invalid Log ID");
                }
                string LogSource = RequestSourceCombo.Text;
                switch (LogSource)
                {
                    case ("Proxy"):
                        if (Config.LastProxyLogId < LogId) throw new Exception("Invalid Log ID");
                        break;
                    case ("Test"):
                        if (Config.LastTestLogId < LogId) throw new Exception("Invalid Log ID");
                        break;
                    case ("Shell"):
                        if (Config.LastShellLogId < LogId) throw new Exception("Invalid Log ID");
                        break;
                    case ("Scan"):
                        if (Config.LastScanLogId < LogId) throw new Exception("Invalid Log ID");
                        break;
                    case ("Probe"):
                        if (Config.LastProbeLogId < LogId) throw new Exception("Invalid Log ID");
                        break;
                    default:
                        if (Config.GetLastLogId(LogSource) < LogId) throw new Exception("Invalid Log ID");
                        break;
                }
            }
            catch
            {
                ShowError("Invalid Log ID");
                RequestSourceIdTB.BackColor = Color.Red;
                return;
            }
            ProcessUserAnswer();
        }

        private void ParameterNameTB_TextChanged(object sender, EventArgs e)
        {
            if (ParameterNameTB.BackColor != Color.White)
            {
                ParameterNameTB.BackColor = Color.White;
            }
        }

        private void ParameterTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ParameterTypeCombo.SelectedIndex == 0)
                ParameterNameTBLbl.Text = "Parameter Index:";
            else
                ParameterNameTBLbl.Text = "Parameter Name:";
        }

        private void ParameterParseRegexTB_TextChanged(object sender, EventArgs e)
        {
            if (ParameterParseRegexTB.BackColor != Color.White)
            {
                ParameterParseRegexTB.BackColor = Color.White;
            }
        }

        private void AddParameterAnswerEntryLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string ParameterSection = ParameterTypeCombo.Text;
            string ParameterName = ParameterNameTB.Text;
            string UpdateFrom = "";
            if (ParameterSourceFromResponseRB.Checked)
                UpdateFrom = "Response";
            if (ParameterSourceFromUserRB.Checked)
                UpdateFrom = "User";
            string ParseResponseMode = "";
            string RegexString = "";
            if (ParameterSourceFromResponseRB.Checked)
            {
                if (ParseParameterFromHtmlRB.Checked)
                    ParseResponseMode = "Html Fields";
                if (ParseParameterFromRegexRB.Checked)
                {
                    ParseResponseMode = "with Regex";
                    try
                    {
                        RegexString = ParameterParseRegexTB.Text.Trim();
                        new Regex(RegexString);
                    }
                    catch
                    {
                        ShowError("Invalid Regex entered");
                        ParameterParseRegexTB.BackColor = Color.Red;
                        return;
                    }
                }
            }
            string Hint = "";
            if (ParameterSourceFromUserRB.Checked)
                Hint = ParameterAskUserHintTB.Text;
            object[] Columns = new object[] { ParameterSection, ParameterName, UpdateFrom, ParseResponseMode, RegexString, Hint };
            foreach (DataGridViewRow Row in ParametersAnswerGrid.Rows)
            {
                if (Row.Cells[0].Value.ToString().Equals(ParameterSection) && Row.Cells[1].Value.ToString().Equals(ParameterName))
                {
                    ShowError("This parameter has already been entered. Remove old entry before adding another one.");
                    return;
                }
            }
            ParametersAnswerGrid.Rows.Add(Columns);
            ParametersAnswerGrid.ClearSelection();

            ResetParameterAnswerFields(false);
            EditParameterAnswerEntryLL.Visible = true;
            DeleteParameterAnswerEntryLL.Visible = true;
        }

        private void EditParameterAnswerEntryLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ParametersAnswerGrid.SelectedRows == null || ParametersAnswerGrid.SelectedRows.Count == 0)
            {
                ShowError("No entry selected. Select a row before deleting it.");
            }
            else
            {
                string ParameterSection = ParametersAnswerGrid.SelectedRows[0].Cells["ParameterSectionColumn"].Value.ToString();
                string ParameterName = ParametersAnswerGrid.SelectedRows[0].Cells["ParameterNameColumn"].Value.ToString();
                string UpdateFrom = ParametersAnswerGrid.SelectedRows[0].Cells["UpdateFromColumn"].Value.ToString();
                string ParseResponseMode = ParametersAnswerGrid.SelectedRows[0].Cells["ParseResponseColumn"].Value.ToString();
                string RegexString = ParametersAnswerGrid.SelectedRows[0].Cells["RegexColumn"].Value.ToString();
                string HintText = ParametersAnswerGrid.SelectedRows[0].Cells["HintColumn"].Value.ToString();
                ParametersAnswerGrid.Rows.RemoveAt(ParametersAnswerGrid.SelectedRows[0].Index);

                ParameterTypeCombo.Text = ParameterSection;
                ParameterNameTB.Text = ParameterName;

                HowToUpdateParameterPanel.Visible = true;

                if (ParametersAnswerAskUserOnly)
                {
                    ParameterSourceFromResponseRB.Checked = false;
                    ParameterSourceFromResponseRB.Enabled = false;
                    HowToParseResponsePanel.Visible = false;
                    UserHintPanel.Visible = true;
                }
                else
                {
                    ParameterSourceFromResponseRB.Enabled = true;
                    ParameterSourceFromUserRB.Enabled = true;
                }
                if (UpdateFrom.Equals("Response"))
                {
                    ParameterSourceFromResponseRB.Checked = true;
                    HowToParseResponsePanel.Visible = true;
                    UserHintPanel.Visible = false;
                }
                else
                {
                    ParameterSourceFromUserRB.Checked = true;
                    HowToParseResponsePanel.Visible = false;
                    UserHintPanel.Visible = true;
                }

                if (ParseResponseMode.Equals("Html Fields"))
                    ParseParameterFromHtmlRB.Checked = true;
                else if (ParseResponseMode.Equals("with Regex"))
                    ParseParameterFromRegexRB.Checked = true;

                ParameterParseRegexTB.Text = RegexString;
                ParameterAskUserHintTB.Text = HintText;
            }
        }

        private void DeleteParameterAnswerEntryLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ParametersAnswerGrid.SelectedRows == null || ParametersAnswerGrid.SelectedRows.Count == 0)
            {
                ShowError("No entry selected. Select a row before deleting it.");
            }
            else
            {
                ParametersAnswerGrid.Rows.RemoveAt(ParametersAnswerGrid.SelectedRows[0].Index);
            }
        }

        private void ParametersAnswerGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ParametersAnswerGrid.SelectedRows == null) return;
            if (ParametersAnswerGrid.SelectedRows.Count == 0) return;
            EditParameterAnswerEntryLL.Enabled = true;
            DeleteParameterAnswerEntryLL.Enabled = true;
        }

        private void ParametersAnswerGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (ParametersAnswerGrid.SelectedRows == null || ParametersAnswerGrid.SelectedRows.Count == 0)
            {
                EditParameterAnswerEntryLL.Enabled = false;
                DeleteParameterAnswerEntryLL.Enabled = false;
            }
            else
            {
                EditParameterAnswerEntryLL.Enabled = true;
                DeleteParameterAnswerEntryLL.Enabled = true;
            }
        }

        private void SubmitParameterAnswerBtn_Click(object sender, EventArgs e)
        {
            ProcessUserAnswer();
        }
        

        private void SpecialOptionBtn_Click(object sender, EventArgs e)
        {
            AnswerTB.Text = "0";
            ProcessUserAnswer();
        }

        private void SelectedOptionSubmitBtn_Click(object sender, EventArgs e)
        {
            ShowError("");
            int SelectedIndex = -1;
            foreach (DataGridViewRow Row in OptionsGrid.Rows)
            {
                if ((bool)Row.Cells[0].Value)
                {
                    SelectedIndex = Row.Index + 1;
                    break;
                }
            }
            if (SelectedIndex < 0)
            {
                ShowError("No options selected!");
            }
            else
            {
                AnswerTB.Text = SelectedIndex.ToString();
                ProcessUserAnswer();
            }
        }

        private void OptionsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow Row in OptionsGrid.Rows)
            {
                if (Row.Selected)
                    Row.Cells[0].Value = true;
                else
                    Row.Cells[0].Value = false;
            }
        }
        #endregion

        private void ShowPseudoCodeGoToMainMenuBtn_Click(object sender, EventArgs e)
        {
            ProcessUserAnswer();
        }

        private void RequestSourceNameTB_TextChanged(object sender, EventArgs e)
        {
            if (RequestSourceNameTB.BackColor != Color.White)
            {
                RequestSourceNameTB.BackColor = Color.White;
            }
        }
    }
}
