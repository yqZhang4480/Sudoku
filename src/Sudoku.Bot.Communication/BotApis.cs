﻿namespace Sudoku.Bot.Communication;

/// <summary>
/// Provides with a set of APIs.
/// </summary>
public static class BotApis
{
	/// <summary>
	/// Gets the details of the bot.
	/// <list type="bullet">
	/// <item>Documentation: <see href="https://bot.q.qq.com/wiki/develop/api/openapi/user/me.html">here</see>.</item>
	/// <item>Corresponding: <c>GET /users/@me</c></item>
	/// <item>Need authorization: false</item>
	/// </list>
	/// </summary>
	public static BotApi 获取用户详情 => new(ApiType.Both, HttpMethod.Get, """/users/@me""");

	/// <summary>
	/// 获取当前用户（机器人）所加入的频道列表
	/// <para>
	/// <see href="https://bot.q.qq.com/wiki/develop/api/openapi/user/guilds.html">接口文档</see><br/>
	/// 无需鉴权<br/>
	/// GET /users/@me/guilds
	/// </para>
	/// </summary>
	public static BotApi 获取用户频道列表 => new(ApiType.Both, HttpMethod.Get, """/users/@me/guilds""");

	/// <summary>
	/// 获取 guild_id 指定的频道的详情
	/// <para>
	/// <see href="https://bot.q.qq.com/wiki/develop/api/openapi/guild/get_guild.html">接口文档</see><br/>
	/// 公域鉴权<br/>
	/// GET /guilds/{guild_id}
	/// </para>
	/// </summary>
	public static BotApi 获取频道详情 => new(ApiType.PublicDomain, HttpMethod.Get, @"/guilds/{guild_id}");

	/// <summary>
	/// 获取 guild_id 指定的频道下的子频道列表
	/// <para>
	/// <see href="https://bot.q.qq.com/wiki/develop/api/openapi/channel/get_channels.html">接口文档</see><br/>
	/// 公域鉴权<br/>
	/// GET /guilds/{guild_id}/channels
	/// </para>
	/// </summary>
	public static BotApi 获取子频道列表 => new(ApiType.PublicDomain, HttpMethod.Get, @"/guilds/{guild_id}/channels");

	/// <summary>
	/// 获取 channel_id 指定的子频道的详情
	/// <para>
	/// <see href="https://bot.q.qq.com/wiki/develop/api/openapi/channel/get_channel.html">接口文档</see><br/>
	/// 公域鉴权<br/>
	/// GET /channels/{channel_id}
	/// </para>
	/// </summary>
	public static BotApi 获取子频道详情 => new(ApiType.PublicDomain, HttpMethod.Get, @"/channels/{channel_id}");

	/// <summary>
	/// 在 guild_id 指定的频道下创建一个子频道
	/// <para>
	/// <see href="https://bot.q.qq.com/wiki/develop/api/openapi/channel/post_channels.html">接口文档</see><br/>
	/// 私域鉴权<br/>
	/// POST /guilds/{guild_id}/channels
	/// </para>
	/// </summary>
	public static BotApi 创建子频道 => new(ApiType.PrivateDomain, HttpMethod.Post, @"/guilds/{guild_id}/channels");

	/// <summary>
	/// 修改 channel_id 指定的子频道的信息
	/// <para>
	/// <see href="https://bot.q.qq.com/wiki/develop/api/openapi/channel/patch_channel.html">接口文档</see><br/>
	/// 私域鉴权<br/>
	/// PATCH /channels/{channel_id}
	/// </para>
	/// </summary>
	public static BotApi 修改子频道 => new(ApiType.PrivateDomain, HttpMethod.Patch, @"/channels/{channel_id}");

	/// <summary>
	/// 删除 channel_id 指定的子频道
	/// <para>
	/// <see href="https://bot.q.qq.com/wiki/develop/api/openapi/channel/delete_channel.html">接口文档</see><br/>
	/// 私域鉴权<br/>
	/// DELETE /channels/{channel_id}
	/// </para>
	/// </summary>
	public static BotApi 删除子频道 => new(ApiType.PrivateDomain, HttpMethod.Delete, @"/channels/{channel_id}");

	/// <summary>
	/// 获取 guild_id 指定的频道中所有成员的详情列表
	/// <para>
	/// <see href="https://bot.q.qq.com/wiki/develop/api/openapi/member/get_members.html">接口文档</see><br/>
	/// 私域鉴权<br/>
	/// GET /guilds/{guild_id}/members
	/// </para>
	/// </summary>
	public static BotApi 获取频道成员列表 => new(ApiType.PrivateDomain, HttpMethod.Get, @"/guilds/{guild_id}/members");
	/// <summary>
	/// 获取 guild_id 指定的频道中 user_id 对应成员的详细信息
	/// <para>
	/// <see href="https://bot.q.qq.com/wiki/develop/api/openapi/member/get_member.html">接口文档</see><br/>
	/// 公域鉴权<br/>
	/// GET /guilds/{guild_id}/members/{user_id}
	/// </para>
	/// </summary>
	public static BotApi 获取成员详情 => new(ApiType.PublicDomain, HttpMethod.Get, @"/guilds/{guild_id}/members/{user_id}");
	/// <summary>
	/// 删除 guild_id 指定的频道中 user_id 对应的成员
	/// <para>
	/// <see href="https://bot.q.qq.com/wiki/develop/api/openapi/member/delete_member.html">接口文档</see><br/>
	/// 私域鉴权<br/>
	/// DELETE /guilds/{guild_id}/members/{user_id}
	/// </para>
	/// </summary>
	public static BotApi 删除频道成员 => new(ApiType.PrivateDomain, HttpMethod.Delete, @"/guilds/{guild_id}/members/{user_id}");

	/// <summary>
	/// 获取 guild_id 指定的频道下的身份组列表
	/// <para>
	/// <see href="https://bot.q.qq.com/wiki/develop/api/openapi/guild/get_guild_roles.html">接口文档</see><br/>
	/// 公域鉴权<br/>
	/// GET /guilds/{guild_id}/roles
	/// </para>
	/// </summary>
	public static BotApi 获取频道身份组列表 => new(ApiType.PublicDomain, HttpMethod.Get, @"/guilds/{guild_id}/roles");
	/// <summary>
	/// 在 guild_id 指定的频道下创建一个身份组
	/// <para>
	/// <see href="https://bot.q.qq.com/wiki/develop/api/openapi/guild/post_guild_role.html">接口文档</see><br/>
	/// 公域鉴权<br/>
	/// POST /guilds/{guild_id}/roles
	/// </para>
	/// </summary>
	public static BotApi 创建频道身份组 => new(ApiType.PublicDomain, HttpMethod.Post, @"/guilds/{guild_id}/roles");
	/// <summary>
	/// 修改频道 guild_id 下 role_id 指定的身份组
	/// <para>
	/// <see href="https://bot.q.qq.com/wiki/develop/api/openapi/guild/patch_guild_role.html">接口文档</see><br/>
	/// 公域鉴权<br/>
	/// PATCH /guilds/{guild_id}/roles/{role_id}
	/// </para>
	/// </summary>
	public static BotApi 修改频道身份组 => new(ApiType.PublicDomain, HttpMethod.Patch, @"/guilds/{guild_id}/roles/{role_id}");
	/// <summary>
	/// 删除频道 guild_id 下 role_id 指定的身份组
	/// <para>
	/// <see href="https://bot.q.qq.com/wiki/develop/api/openapi/guild/delete_guild_role.html">接口文档</see><br/>
	/// 公域鉴权<br/>
	/// DELETE /guilds/{guild_id}/roles/{role_id}
	/// </para>
	/// </summary>
	public static BotApi 删除频道身份组 => new(ApiType.PublicDomain, HttpMethod.Delete, @"/guilds/{guild_id}/roles/{role_id}");
	/// <summary>
	/// 将频道 guild_id 下的用户 user_id 添加到身份组 role_id
	/// <para>
	/// <see href="https://bot.q.qq.com/wiki/develop/api/openapi/guild/put_guild_member_role.html">接口文档</see><br/>
	/// 公域鉴权<br/>
	/// PUT /guilds/{guild_id}/members/{user_id}/roles/{role_id}
	/// </para>
	/// </summary>
	public static BotApi 添加频道身份组成员 => new(ApiType.PublicDomain, HttpMethod.Put, @"/guilds/{guild_id}/members/{user_id}/roles/{role_id}");
	/// <summary>
	/// 将用户 user_id 从频道 guild_id 的 role_id 身份组中移除
	/// <para>
	/// <see href="https://bot.q.qq.com/wiki/develop/api/openapi/guild/delete_guild_member_role.html">接口文档</see><br/>
	/// 公域鉴权<br/>
	/// DELETE /guilds/{guild_id}/members/{user_id}/roles/{role_id}
	/// </para>
	/// </summary>
	public static BotApi 删除频道身份组成员 => new(ApiType.PublicDomain, HttpMethod.Delete, @"/guilds/{guild_id}/members/{user_id}/roles/{role_id}");

	/// <summary>
	/// 获取子频道 channel_id 下用户 user_id 的权限
	/// <para>
	/// <see href="https://bot.q.qq.com/wiki/develop/api/openapi/channel_permissions/get_channel_permissions.html">接口文档</see><br/>
	/// 公域鉴权<br/>
	/// GET /channels/{channel_id}/members/{user_id}/permissions
	/// </para>
	/// </summary>
	public static BotApi 获取子频道用户权限 => new(ApiType.PublicDomain, HttpMethod.Get, @"/channels/{channel_id}/members/{user_id}/permissions");
	/// <summary>
	/// 修改子频道 channel_id 下用户 user_id 的权限
	/// <para>
	/// <see href="https://bot.q.qq.com/wiki/develop/api/openapi/channel_permissions/put_channel_permissions.html">接口文档</see><br/>
	/// 公域鉴权<br/>
	/// PUT /channels/{channel_id}/members/{user_id}/permissions
	/// </para>
	/// </summary>
	public static BotApi 修改子频道用户权限 => new(ApiType.PublicDomain, HttpMethod.Put, @"/channels/{channel_id}/members/{user_id}/permissions");
	/// <summary>
	/// 获取子频道 channel_id 下身份组 role_id 的权限
	/// <para>
	/// <see href="https://bot.q.qq.com/wiki/develop/api/openapi/channel_permissions/get_channel_roles_permissions.html">接口文档</see><br/>
	/// 公域鉴权<br/>
	/// GET /channels/{channel_id}/roles/{role_id}/permissions
	/// </para>
	/// </summary>
	public static BotApi 获取子频道身份组权限 => new(ApiType.PublicDomain, HttpMethod.Get, @"/channels/{channel_id}/roles/{role_id}/permissions");
	/// <summary>
	/// 修改子频道 channel_id 下身份组 role_id 的权限
	/// <para>
	/// <see href="https://bot.q.qq.com/wiki/develop/api/openapi/channel_permissions/put_channel_roles_permissions.html">接口文档</see><br/>
	/// 公域鉴权<br/>
	/// PUT /channels/{channel_id}/roles/{role_id}/permissions
	/// </para>
	/// </summary>
	public static BotApi 修改子频道身份组权限 => new(ApiType.PublicDomain, HttpMethod.Put, @"/channels/{channel_id}/roles/{role_id}/permissions");

	/// <summary>
	/// 获取子频道 channel_id 下的消息 message_id 的详情
	/// <para>
	/// <see href="https://bot.q.qq.com/wiki/develop/api/openapi/message/get_message_of_id.html">接口文档</see><br/>
	/// 公域鉴权<br/>
	/// GET /channels/{channel_id}/messages/{message_id}
	/// </para>
	/// </summary>
	public static BotApi 获取指定消息 => new(ApiType.PublicDomain, HttpMethod.Get, @"/channels/{channel_id}/messages/{message_id}");
	/// <summary>
	/// 获取子频道 channel_id 下的消息列表
	/// <para>
	/// <see href="https://bot.q.qq.com/wiki/develop/pythonsdk/api/message/get_messages.html">接口文档</see><br/>
	/// 私域鉴权<br/>
	/// GET /channels/{channel_id}/messages
	/// </para>
	/// </summary>
	public static BotApi 获取消息列表 => new(ApiType.PrivateDomain, HttpMethod.Get, @"/channels/{channel_id}/messages");
	/// <summary>
	/// 向 channel_id 指定的子频道发送消息
	/// <para>
	/// <see href="https://bot.q.qq.com/wiki/develop/api/openapi/message/post_messages.html">接口文档</see><br/>
	/// 公域鉴权<br/>
	/// POST /channels/{channel_id}/messages
	/// </para>
	/// </summary>
	public static BotApi 发送消息 => new(ApiType.PublicDomain, HttpMethod.Post, @"/channels/{channel_id}/messages");
	/// <summary>
	/// 撤回 message_id 指定的消息
	/// <para>
	/// <see href="https://bot.q.qq.com/wiki/develop/nodesdk/message/delete_message.html">接口文档</see><br/>
	/// 私域鉴权<br/>
	/// DELETE /channels/{channel_id}/messages/{message_id}
	/// </para>
	/// </summary>
	public static BotApi 撤回消息 => new(ApiType.PrivateDomain, HttpMethod.Delete, @"/channels/{channel_id}/messages/{message_id}");

	/// <summary>
	/// 机器人和在同一个频道内的成员创建私信会话
	/// <para>
	/// <see href="https://bot.q.qq.com/wiki/develop/api/openapi/dms/post_dms.html">接口文档</see><br/>
	/// 公域鉴权<br/>
	/// POST /users/@me/dms
	/// </para>
	/// </summary>
	public static BotApi 创建私信会话 => new(ApiType.PublicDomain, HttpMethod.Post, @"/users/@me/dms");
	/// <summary>
	/// 发送私信消息（已经创建私信会话后）
	/// <para>
	/// <see href="https://bot.q.qq.com/wiki/develop/api/openapi/dms/post_dms_messages.html">接口文档</see><br/>
	/// 公域鉴权<br/>
	/// POST /dms/{guild_id}/messages
	/// </para>
	/// </summary>
	public static BotApi 发送私信 => new(ApiType.PublicDomain, HttpMethod.Post, @"/dms/{guild_id}/messages");

	/// <summary>
	/// 将频道的全体成员（非管理员）禁言
	/// <para>
	/// <see href="https://bot.q.qq.com/wiki/develop/api/openapi/guild/patch_guild_mute.html">接口文档</see><br/>
	/// 公域鉴权<br/>
	/// PATCH /guilds/{guild_id}/mute
	/// </para>
	/// </summary>
	public static BotApi 禁言全员 => new(ApiType.PublicDomain, HttpMethod.Patch, @"/guilds/{guild_id}/mute");
	/// <summary>
	/// 禁言频道 guild_id 下的成员 user_id
	/// <para>
	/// <see href="https://bot.q.qq.com/wiki/develop/api/openapi/guild/patch_guild_member_mute.html">接口文档</see><br/>
	/// 公域鉴权<br/>
	/// PATCH /guilds/{guild_id}/members/{user_id}/mute
	/// </para>
	/// </summary>
	public static BotApi 禁言指定成员 => new(ApiType.PublicDomain, HttpMethod.Patch, @"/guilds/{guild_id}/members/{user_id}/mute");

	/// <summary>
	/// 将频道 guild_id 内的某条消息设置为频道全局公告
	/// <para>
	/// <see href="https://bot.q.qq.com/wiki/develop/api/openapi/announces/post_guild_announces.html">接口文档</see><br/>
	/// 公域鉴权<br/>
	/// POST /guilds/{guild_id}/announces
	/// </para>
	/// </summary>
	public static BotApi 创建频道公告 => new(ApiType.PublicDomain, HttpMethod.Post, @"/guilds/{guild_id}/announces");
	/// <summary>
	/// 删除频道 guild_id 下 message_id 指定的全局公告
	/// <para>
	/// <see href="https://bot.q.qq.com/wiki/develop/api/openapi/announces/delete_guild_announces.html">接口文档</see><br/>
	/// 公域鉴权<br/>
	/// DELETE /guilds/{guild_id}/announces/{message_id}
	/// </para>
	/// </summary>
	public static BotApi 删除频道公告 => new(ApiType.PublicDomain, HttpMethod.Delete, @"/guilds/{guild_id}/announces/{message_id}");
	/// <summary>
	/// 将子频道 channel_id 内的某条消息设置为子频道公告
	/// <para>
	/// <see href="https://bot.q.qq.com/wiki/develop/api/openapi/announces/post_channel_announces.html">接口文档</see><br/>
	/// 公域鉴权<br/>
	/// POST /channels/{channel_id}/announces
	/// </para>
	/// </summary>
	public static BotApi 创建子频道公告 => new(ApiType.PublicDomain, HttpMethod.Post, @"/channels/{channel_id}/announces");
	/// <summary>
	/// 删除子频道 channel_id 下 message_id 指定的子频道公告
	/// <para>
	/// <see href="https://bot.q.qq.com/wiki/develop/api/openapi/announces/delete_channel_announces.html">接口文档</see><br/>
	/// 公域鉴权<br/>
	/// DELETE /channels/{channel_id}/announces/{message_id}
	/// </para>
	/// </summary>
	public static BotApi 删除子频道公告 => new(ApiType.PublicDomain, HttpMethod.Delete, @"/channels/{channel_id}/announces/{message_id}");

	/// <summary>
	/// 获取 channel_id 指定的子频道中当天的日程列表
	/// <para>
	/// <see href="https://bot.q.qq.com/wiki/develop/api/openapi/schedule/get_schedules.html">接口文档</see><br/>
	/// 公域鉴权<br/>
	/// GET /channels/{channel_id}/schedules
	/// </para>
	/// </summary>
	public static BotApi 获取频道日程列表 => new(ApiType.PublicDomain, HttpMethod.Get, @"/channels/{channel_id}/schedules");
	/// <summary>
	/// 获取日程子频道 channel_id 下 schedule_id 指定的的日程的详情
	/// <para>
	/// <see href="https://bot.q.qq.com/wiki/develop/api/openapi/schedule/get_schedule.html">接口文档</see><br/>
	/// 公域鉴权<br/>
	/// GET /channels/{channel_id}/schedules/{schedule_id}
	/// </para>
	/// </summary>
	public static BotApi 获取日程详情 => new(ApiType.PublicDomain, HttpMethod.Get, @"/channels/{channel_id}/schedules/{schedule_id}");
	/// <summary>
	/// 在 channel_id 指定的日程子频道下创建一个日程
	/// <para>
	/// <see href="https://bot.q.qq.com/wiki/develop/api/openapi/schedule/post_schedule.html">接口文档</see><br/>
	/// 公域鉴权<br/>
	/// POST /channels/{channel_id}/schedules
	/// </para>
	/// </summary>
	public static BotApi 创建日程 => new(ApiType.PublicDomain, HttpMethod.Post, @"/channels/{channel_id}/schedules");
	/// <summary>
	/// 修改日程子频道 channel_id 下 schedule_id 指定的日程的详情
	/// <para>
	/// <see href="https://bot.q.qq.com/wiki/develop/api/openapi/schedule/patch_schedule.html">接口文档</see><br/>
	/// 公域鉴权<br/>
	/// PATCH /channels/{channel_id}/schedules/{schedule_id}
	/// </para>
	/// </summary>
	public static BotApi 修改日程 => new(ApiType.PublicDomain, HttpMethod.Patch, @"/channels/{channel_id}/schedules/{schedule_id}");
	/// <summary>
	/// 删除日程子频道 channel_id 下 schedule_id 指定的日程
	/// <para>
	/// <see href="https://bot.q.qq.com/wiki/develop/api/openapi/schedule/delete_schedule.html">接口文档</see><br/>
	/// 公域鉴权<br/>
	/// DELETE /channels/{channel_id}/schedules/{schedule_id}
	/// </para>
	/// </summary>
	public static BotApi 删除日程 => new(ApiType.PublicDomain, HttpMethod.Delete, @"/channels/{channel_id}/schedules/{schedule_id}");

	/// <summary>
	/// 控制子频道 channel_id 下的音频
	/// <para>
	/// <see href="https://bot.q.qq.com/wiki/develop/api/openapi/audio/audio_control.html">接口文档</see><br/>
	/// 公域鉴权<br/>
	/// POST /channels/{channel_id}/audio
	/// </para>
	/// </summary>
	public static BotApi 音频控制 => new(ApiType.PublicDomain, HttpMethod.Post, @"/channels/{channel_id}/audio");

	/// <summary>
	/// 获取机器人在频道 guild_id 内可以使用的权限列表
	/// <para>
	/// <see href="https://bot.q.qq.com/wiki/develop/api/openapi/api_permissions/get_guild_api_permission.html">接口文档</see><br/>
	/// 无需鉴权<br/>
	/// GET /guilds/{guild_id}/api_permission
	/// </para>
	/// </summary>
	public static BotApi 获取频道可用权限列表 => new(ApiType.Both, HttpMethod.Get, @"/guilds/{guild_id}/api_permission");
	/// <summary>
	/// 创建 API 接口权限授权链接，该链接指向 guild_id 对应的频道
	/// <para>
	/// <see href="https://bot.q.qq.com/wiki/develop/api/openapi/api_permissions/post_api_permission_demand.html">接口文档</see><br/>
	/// 无需鉴权<br/>
	/// POST /guilds/{guild_id}/api_permission/demand
	/// </para>
	/// </summary>
	public static BotApi 创建频道接口授权链接 => new(ApiType.Both, HttpMethod.Post, @"/guilds/{guild_id}/api_permission/demand");
}
