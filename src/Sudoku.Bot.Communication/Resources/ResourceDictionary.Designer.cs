﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sudoku.Bot.Communication.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ResourceDictionary {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ResourceDictionary() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Sudoku.Bot.Communication.Resources.ResourceDictionary", typeof(ResourceDictionary).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to P:\Bot\bot.json.
        /// </summary>
        internal static string @__LocalBotConfigPath {
            get {
                return ResourceManager.GetString("__LocalBotConfigPath", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to P:\Bot\Players.
        /// </summary>
        internal static string @__LocalPlayerConfigPath {
            get {
                return ResourceManager.GetString("__LocalPlayerConfigPath", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 向向的游戏机器人
        ///作者：SunnieShine（小向）
        ///版本：.
        /// </summary>
        internal static string AboutInfo_Segment1 {
            get {
                return ResourceManager.GetString("AboutInfo_Segment1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 玩法：请通过艾特本机器人，并带上斜杠符号。支持的指令有：
        ///💡 /数独：快来和朋友们一起 PK 数独吧！
        ///💡 /签到：允许玩家每日参与一次签到~
        ///💡 /排名：对玩家的经验值获得情况进行排名~
        ///💡 /关于：显示各个指令的介绍信息~.
        /// </summary>
        internal static string AboutInfo_Segment2 {
            get {
                return ResourceManager.GetString("AboutInfo_Segment2", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 服务端鉴权成功.
        /// </summary>
        internal static string AuthorizationSuccessful {
            get {
                return ResourceManager.GetString("AuthorizationSuccessful", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 连接服务器失败，重试次数已经用光。请重新调试代码，联系 QQ 客服以尽快定位错误发生以及 bug 触发情况。.
        /// </summary>
        internal static string BotConnectedCallbackOutput_Fail {
            get {
                return ResourceManager.GetString("BotConnectedCallbackOutput_Fail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 连接服务器成功！请使用 Ctrl + C 组合按键来终止程序运行。.
        /// </summary>
        internal static string BotConnectedCallbackOutput_Success {
            get {
                return ResourceManager.GetString("BotConnectedCallbackOutput_Success", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 机器人已加入.
        /// </summary>
        internal static string BotHasJoined {
            get {
                return ResourceManager.GetString("BotHasJoined", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 客户端.
        /// </summary>
        internal static string Client {
            get {
                return ResourceManager.GetString("Client", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 签到失败。原因：
        ///用户存档文件里的此数据为 null 值。机器人设计者，你看看你都干了些什么好事！.
        /// </summary>
        internal static string ClockInError_DateStringIsNull {
            get {
                return ResourceManager.GetString("ClockInError_DateStringIsNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 对不起，机器人出了点问题。原因：
        ///机器人尝试在读取用户的存档的时候，“日期”一行的文字并不是真的日期的字符串数据，而是别的什么东西。可能在录入数据的时候就出现了问题。请联系机器人设计者修复此问题。.
        /// </summary>
        internal static string ClockInError_InvalidDateValue {
            get {
                return ResourceManager.GetString("ClockInError_InvalidDateValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 对不起，机器人出了点问题。原因：
        ///机器人尚未配置用户存档数据的本地路径。该路径对于机器人识别用户的数据来说非常重要。如果没有此数据的话，机器人就相当于直接不知道怎么读写你们的“存档”了。这是很可怕的，对吧。.
        /// </summary>
        internal static string ClockInError_ResourceNotFound {
            get {
                return ResourceManager.GetString("ClockInError_ResourceNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 签到成功。这是你第一次使用机器人哦~ 恭喜你获得.
        /// </summary>
        internal static string ClockInSuccess_FileCreatedSegment1 {
            get {
                return ResourceManager.GetString("ClockInSuccess_FileCreatedSegment1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 经验值~.
        /// </summary>
        internal static string ClockInSuccess_FileCreatedSegment2 {
            get {
                return ResourceManager.GetString("ClockInSuccess_FileCreatedSegment2", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 签到成功。这是你第一次签到哦~ 恭喜你获得.
        /// </summary>
        internal static string ClockInSuccess_ValueCreatedSegment1 {
            get {
                return ResourceManager.GetString("ClockInSuccess_ValueCreatedSegment1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 经验值~ 你现在拥有.
        /// </summary>
        internal static string ClockInSuccess_ValueCreatedSegment2 {
            get {
                return ResourceManager.GetString("ClockInSuccess_ValueCreatedSegment2", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 经验值~.
        /// </summary>
        internal static string ClockInSuccess_ValueCreatedSegment3 {
            get {
                return ResourceManager.GetString("ClockInSuccess_ValueCreatedSegment3", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 签到成功。数据已更新~ 恭喜你获得.
        /// </summary>
        internal static string ClockInSuccess_ValueUpdatedSegment1 {
            get {
                return ResourceManager.GetString("ClockInSuccess_ValueUpdatedSegment1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 经验值~ 你现在拥有.
        /// </summary>
        internal static string ClockInSuccess_ValueUpdatedSegment2 {
            get {
                return ResourceManager.GetString("ClockInSuccess_ValueUpdatedSegment2", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 经验值~.
        /// </summary>
        internal static string ClockInSuccess_ValueUpdatedSegment3 {
            get {
                return ResourceManager.GetString("ClockInSuccess_ValueUpdatedSegment3", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 签到失败。原因：不能在同一天签到多次。.
        /// </summary>
        internal static string ClockInWarning_CannotClockInInSameDay {
            get {
                return ResourceManager.GetString("ClockInWarning_CannotClockInInSameDay", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ）.
        /// </summary>
        internal static string CloseBrace {
            get {
                return ResourceManager.GetString("CloseBrace", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ：.
        /// </summary>
        internal static string Colon {
            get {
                return ResourceManager.GetString("Colon", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ，.
        /// </summary>
        internal static string Comma {
            get {
                return ResourceManager.GetString("Comma", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 关于.
        /// </summary>
        internal static string Command_About {
            get {
                return ResourceManager.GetString("Command_About", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 签到.
        /// </summary>
        internal static string Command_ClockIn {
            get {
                return ResourceManager.GetString("Command_ClockIn", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 排名.
        /// </summary>
        internal static string Command_Rank {
            get {
                return ResourceManager.GetString("Command_Rank", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 数独.
        /// </summary>
        internal static string Command_Sudoku {
            get {
                return ResourceManager.GetString("Command_Sudoku", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 已存在。该指令将会被忽略掉。.
        /// </summary>
        internal static string CommandAlreadyExists {
            get {
                return ResourceManager.GetString("CommandAlreadyExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 不存在。.
        /// </summary>
        internal static string CommandNotExist {
            get {
                return ResourceManager.GetString("CommandNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 该指令不需要后面跟上别的东西。为了规避错误处理而导致的不可解决的问题，请删去它们并重试一次。.
        /// </summary>
        internal static string CommandParserError_CommandNotRequireParameter {
            get {
                return ResourceManager.GetString("CommandParserError_CommandNotRequireParameter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 已注册。.
        /// </summary>
        internal static string CommandRegisteredSuccessfully {
            get {
                return ResourceManager.GetString("CommandRegisteredSuccessfully", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 删除失败。.
        /// </summary>
        internal static string CommandRemovedFailed {
            get {
                return ResourceManager.GetString("CommandRemovedFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 已删除。.
        /// </summary>
        internal static string CommandRemovedSuccessfully {
            get {
                return ResourceManager.GetString("CommandRemovedSuccessfully", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 指令.
        /// </summary>
        internal static string CommandText {
            get {
                return ResourceManager.GetString("CommandText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 重连次数已耗尽，无法与频道服务器建立连接！.
        /// </summary>
        internal static string ConnectFailed_MaxRetryCountReached {
            get {
                return ResourceManager.GetString("ConnectFailed_MaxRetryCountReached", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 已恢复与服务器的连接.
        /// </summary>
        internal static string ConnectionIsResumed {
            get {
                return ResourceManager.GetString("ConnectionIsResumed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 未建立连接！.
        /// </summary>
        internal static string ConnectionNotHavingBeenCreated {
            get {
                return ResourceManager.GetString("ConnectionNotHavingBeenCreated", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 建立连接失败！.
        /// </summary>
        internal static string ConnectToServerFailed {
            get {
                return ResourceManager.GetString("ConnectToServerFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 日.
        /// </summary>
        internal static string DateTimeUnit_Day1 {
            get {
                return ResourceManager.GetString("DateTimeUnit_Day1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 天.
        /// </summary>
        internal static string DateTimeUnit_Day2 {
            get {
                return ResourceManager.GetString("DateTimeUnit_Day2", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 小时.
        /// </summary>
        internal static string DateTimeUnit_Hour1 {
            get {
                return ResourceManager.GetString("DateTimeUnit_Hour1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 时.
        /// </summary>
        internal static string DateTimeUnit_Hour2 {
            get {
                return ResourceManager.GetString("DateTimeUnit_Hour2", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 分钟.
        /// </summary>
        internal static string DateTimeUnit_Minute1 {
            get {
                return ResourceManager.GetString("DateTimeUnit_Minute1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 分.
        /// </summary>
        internal static string DateTimeUnit_Minute2 {
            get {
                return ResourceManager.GetString("DateTimeUnit_Minute2", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 秒钟.
        /// </summary>
        internal static string DateTimeUnit_Second1 {
            get {
                return ResourceManager.GetString("DateTimeUnit_Second1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 秒.
        /// </summary>
        internal static string DateTimeUnit_Second2 {
            get {
                return ResourceManager.GetString("DateTimeUnit_Second2", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 星期.
        /// </summary>
        internal static string DateTimeUnit_Week1 {
            get {
                return ResourceManager.GetString("DateTimeUnit_Week1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 周.
        /// </summary>
        internal static string DateTimeUnit_Week2 {
            get {
                return ResourceManager.GetString("DateTimeUnit_Week2", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 年.
        /// </summary>
        internal static string DateTimeUnit_Year {
            get {
                return ResourceManager.GetString("DateTimeUnit_Year", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 新的日程.
        /// </summary>
        internal static string DefaultScheduleDescription {
            get {
                return ResourceManager.GetString("DefaultScheduleDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 新建日程.
        /// </summary>
        internal static string DefaultScheduleName {
            get {
                return ResourceManager.GetString("DefaultScheduleName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 描述：.
        /// </summary>
        internal static string Description {
            get {
                return ResourceManager.GetString("Description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 第.
        /// </summary>
        internal static string Di {
            get {
                return ResourceManager.GetString("Di", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ❌.
        /// </summary>
        internal static string Emoji_Cross {
            get {
                return ResourceManager.GetString("Emoji_Cross", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 经验值.
        /// </summary>
        internal static string ExperiencePointText {
            get {
                return ResourceManager.GetString("ExperiencePointText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 获取已加入的频道列表，第.
        /// </summary>
        internal static string GetGuildJoined {
            get {
                return ResourceManager.GetString("GetGuildJoined", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 页为空，操作结束.
        /// </summary>
        internal static string GetGuildJoinedEmpty {
            get {
                return ResourceManager.GetString("GetGuildJoinedEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 页失败.
        /// </summary>
        internal static string GetGuildJoinedFailed {
            get {
                return ResourceManager.GetString("GetGuildJoinedFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 页成功，数量.
        /// </summary>
        internal static string GetGuildJoinedSuccessful {
            get {
                return ResourceManager.GetString("GetGuildJoinedSuccessful", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 获取已加入的频道列表，分页大小：100.
        /// </summary>
        internal static string GetGuildListWithLimit100 {
            get {
                return ResourceManager.GetString("GetGuildListWithLimit100", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 个频道.
        /// </summary>
        internal static string GuildCountText {
            get {
                return ResourceManager.GetString("GuildCountText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to (\d+)\s*(年|星期|周|日|天|小?时|分钟?|秒钟?).
        /// </summary>
        internal static string JinxTimeCountdownPattern {
            get {
                return ResourceManager.GetString("JinxTimeCountdownPattern", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to (\d{4})[-年](\d\d)[-月](\d\d)[\s日]*(\d\d)[:点时](\d\d)[:分](\d\d)秒?.
        /// </summary>
        internal static string JinxTimestampPattern {
            get {
                return ResourceManager.GetString("JinxTimestampPattern", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ，详情：.
        /// </summary>
        internal static string LogContent_Details {
            get {
                return ResourceManager.GetString("LogContent_Details", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 代码：.
        /// </summary>
        internal static string LogContent_ErrorCode {
            get {
                return ResourceManager.GetString("LogContent_ErrorCode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 异常代码：.
        /// </summary>
        internal static string LogContent_ErrorCodeText {
            get {
                return ResourceManager.GetString("LogContent_ErrorCodeText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 异常详情：.
        /// </summary>
        internal static string LogContent_ErrorDetails {
            get {
                return ResourceManager.GetString("LogContent_ErrorDetails", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 访问接口的主体不是.
        /// </summary>
        internal static string LogContent_InterfaceMainBodyNotSenderSegment1 {
            get {
                return ResourceManager.GetString("LogContent_InterfaceMainBodyNotSenderSegment1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ，取消推送给发件人。.
        /// </summary>
        internal static string LogContent_InterfaceMainBodyNotSenderSegment2 {
            get {
                return ResourceManager.GetString("LogContent_InterfaceMainBodyNotSenderSegment2", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 被动可回复时间已超时，取消推送给发件人。.
        /// </summary>
        internal static string LogContent_MessageTimeout {
            get {
                return ResourceManager.GetString("LogContent_MessageTimeout", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 机器人配置.
        /// </summary>
        internal static string LogContent_ReportFalseSegment1 {
            get {
                return ResourceManager.GetString("LogContent_ReportFalseSegment1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 属性为 false，不报告错误。已经自动取消推送给发件人。.
        /// </summary>
        internal static string LogContent_ReportFalseSegment2 {
            get {
                return ResourceManager.GetString("LogContent_ReportFalseSegment2", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 请求方式：.
        /// </summary>
        internal static string LogContent_RequestMethod {
            get {
                return ResourceManager.GetString("LogContent_RequestMethod", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 请求地址：.
        /// </summary>
        internal static string LogContent_RequestUrl {
            get {
                return ResourceManager.GetString("LogContent_RequestUrl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 解冻时间：.
        /// </summary>
        internal static string LogContent_UnfrozenTimeLast {
            get {
                return ResourceManager.GetString("LogContent_UnfrozenTimeLast", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 接口冻结：暂停使用此接口.
        /// </summary>
        internal static string LogContent_WindowIsFrozen {
            get {
                return ResourceManager.GetString("LogContent_WindowIsFrozen", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 接口访问失败.
        /// </summary>
        internal static string LogHeader_InterfaceAccessFailed {
            get {
                return ResourceManager.GetString("LogHeader_InterfaceAccessFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 名.
        /// </summary>
        internal static string Ming {
            get {
                return ResourceManager.GetString("Ming", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 管理员.
        /// </summary>
        internal static string Name_Administrator {
            get {
                return ResourceManager.GetString("Name_Administrator", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 子频道管理员.
        /// </summary>
        internal static string Name_ChannelManager {
            get {
                return ResourceManager.GetString("Name_ChannelManager", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 频道主.
        /// </summary>
        internal static string Name_GuildOwner {
            get {
                return ResourceManager.GetString("Name_GuildOwner", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 普通成员.
        /// </summary>
        internal static string Name_NormalMember {
            get {
                return ResourceManager.GetString("Name_NormalMember", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 私域机器人.
        /// </summary>
        internal static string PrivateDomainBotSuffix {
            get {
                return ResourceManager.GetString("PrivateDomainBotSuffix", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 公域机器人.
        /// </summary>
        internal static string PublicDomainBotSuffix {
            get {
                return ResourceManager.GetString("PublicDomainBotSuffix", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 本地路径尚未存储任何用户的数据，无法排名。.
        /// </summary>
        internal static string RankExpFailed_NoConfigFileFound {
            get {
                return ResourceManager.GetString("RankExpFailed_NoConfigFileFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 我们来看一下经验值排名吧！.
        /// </summary>
        internal static string RankExpSuccessful_Segment1 {
            get {
                return ResourceManager.GetString("RankExpSuccessful_Segment1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 客户端鉴权信息错误.
        /// </summary>
        internal static string ReceiveAuthorizationErrorMessage {
            get {
                return ResourceManager.GetString("ReceiveAuthorizationErrorMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 服务器收到心跳包.
        /// </summary>
        internal static string ReceiveHeartbeatAcknowledgementMessage {
            get {
                return ResourceManager.GetString("ReceiveHeartbeatAcknowledgementMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 成功与网关建立连接.
        /// </summary>
        internal static string ReceiveHelloMessage {
            get {
                return ResourceManager.GetString("ReceiveHelloMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 服务器要求客户端重连.
        /// </summary>
        internal static string ReceiveReconnectMessage {
            get {
                return ResourceManager.GetString("ReceiveReconnectMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 恢复连接失败：.
        /// </summary>
        internal static string ResumeError {
            get {
                return ResourceManager.GetString("ResumeError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SDK 版本.
        /// </summary>
        internal static string SdkVersionName {
            get {
                return ResourceManager.GetString("SdkVersionName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 秒后再次尝试连接（剩余重试次数：.
        /// </summary>
        internal static string SecondsLastToRetry {
            get {
                return ResourceManager.GetString("SecondsLastToRetry", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 客户端发起鉴权.
        /// </summary>
        internal static string SendAuthorization {
            get {
                return ResourceManager.GetString("SendAuthorization", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 发送心跳包.
        /// </summary>
        internal static string SendHeartbeat {
            get {
                return ResourceManager.GetString("SendHeartbeat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 客户端尝试恢复连接....
        /// </summary>
        internal static string SendResumeMessage {
            get {
                return ResourceManager.GetString("SendResumeMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 服务器.
        /// </summary>
        internal static string Server {
            get {
                return ResourceManager.GetString("Server", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 状态：.
        /// </summary>
        internal static string Status {
            get {
                return ResourceManager.GetString("Status", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 重新建立到服务器的连接....
        /// </summary>
        internal static string TryReconnectToServer {
            get {
                return ResourceManager.GetString("TryReconnectToServer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 未知事件.
        /// </summary>
        internal static string UnknownEvent {
            get {
                return ResourceManager.GetString("UnknownEvent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 未知操作码：.
        /// </summary>
        internal static string UnkownOperationCode {
            get {
                return ResourceManager.GetString("UnkownOperationCode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 使用网关地址.
        /// </summary>
        internal static string UseGatewayAddress {
            get {
                return ResourceManager.GetString("UseGatewayAddress", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 此错误类型未被收录。.
        /// </summary>
        internal static string WebSocketError_SuchApiErrorNotFound {
            get {
                return ResourceManager.GetString("WebSocketError_SuchApiErrorNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to WebSocket 消息类型：.
        /// </summary>
        internal static string WebSocketMessageTypeIs {
            get {
                return ResourceManager.GetString("WebSocketMessageTypeIs", resourceCulture);
            }
        }
    }
}
