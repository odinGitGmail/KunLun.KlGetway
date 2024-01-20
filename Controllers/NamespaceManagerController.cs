using System.ComponentModel.DataAnnotations;
using Cola.ColaNacos;
using Cola.ColaWebApi;
using Cola.Core.Models;
using Cola.Core.Models.ColaNacos;
using Microsoft.AspNetCore.Mvc;

namespace KunLun.KlGetway.Controllers;

[ApiController]
[Route("nacos")]
public class NamespaceManagerController: ControllerBase
{
    private readonly IWebApi _webApi;
    private readonly IColaNacos _colaNacos;
    public NamespaceManagerController(IWebApi webApi, IColaNacos colaNacos)
    {
        _webApi = webApi.GetClient("ColaNacos");
        _colaNacos = colaNacos;
    }

    /// <summary>
    /// 查询命名空间列表
    /// </summary>
    /// <returns>ApiResult&lt;List&lt;NamespaceList&gt;&gt;</returns>
    [HttpGet]
    [Route("v{majorVersion}/console/namespace/list")]
    public ApiResult<List<NacosNamespaceInfo>>? GetAllNamespace()
    {
        return _colaNacos.NamespaceQuery("ColaNacos");
    }
    
    /// <summary>
    /// 查询命名空间列表
    /// </summary>
    /// <returns>ApiResult&lt;NamespaceList&gt;</returns>
    [HttpGet]
    [Route("v{majorVersion}/console/namespace")]
    public ApiResult<NacosNamespaceInfo>? GetNamespaceById(string? namespaceId)
    {
        return _colaNacos.NamespaceQueryById("ColaNacos",namespaceId);
    }
    
    /// <summary>
    /// 创建命名空间
    /// </summary>
    /// <param name="nacosNamespace">请求Body</param>
    /// <returns>是否执行成功</returns>
    [HttpPost]
    [Route("v{majorVersion}/console/namespace")]
    public ApiResult<bool>? AddNamespace([FromBody] NacosNamespace nacosNamespace)
    {
        return _colaNacos.CreateNamespace("ColaNacos",nacosNamespace);
    }
    
    /// <summary>
    /// 编辑命名空间
    /// </summary>
    /// <param name="nacosNamespace">请求Body</param>
    /// <returns>是否执行成功</returns>
    [HttpPut]
    [Route("v{majorVersion}/console/namespace")]
    public ApiResult<bool>? UpdateNamespace([FromBody] NacosNamespace nacosNamespace)
    {
        return _colaNacos.UpdateNamespace("ColaNacos",nacosNamespace);
    }
    
    /// <summary>
    /// 删除命名空间
    /// </summary>
    /// <param name="namespaceId">namespaceId</param>
    /// <returns>是否执行成功</returns>
    [HttpDelete]
    [Route("v{majorVersion}/console/namespace")]
    public ApiResult<bool>? DeleteNamespace([FromQuery][Required] string namespaceId)
    {
        return _colaNacos.DeleteNamespace("ColaNacos", namespaceId);
    }
}