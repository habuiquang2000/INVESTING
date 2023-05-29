using BaseLib.Dtos;
using BaseLib.Dtos.User;
using BaseLib.Https;
using BaseLib.Models;
using Gateway.BLL;
using Gateway.Helper;
using Gateway.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Implementations;

public class CUserGateway : IUserHandler
{
    private readonly IConfiguration _configuration;
    private readonly BaseHttpClient _baseHttpClient;
    public CUserGateway(IConfiguration configuration)
    {
        _configuration = configuration;
        _baseHttpClient = new(_configuration.GetSection(CConfigAG.HOST_PRODUCT).Value);
    }

    public async Task<EResponse> Api_CheckLoginUserAsync(string? accessToken)
    {
        try
        {
            UserModel userToken = BaseAuthentication.GetClaims(accessToken!);
            ERequest? requestCheck = await _baseHttpClient.PostAsync<ERequest>(
                _configuration.GetSection(CConfigAG.ENDPOINT_CHECK_USERNAME).Value,
                userToken
            );
            ERequest.LOGIN_CODE loginStatus = (ERequest.LOGIN_CODE)Enum.Parse(
                typeof(ERequest.LOGIN_CODE),
                (requestCheck!.Data ?? "").ToString() ?? ""
            );
            if (loginStatus == ERequest.LOGIN_CODE.NOT_EXIST)
            {
                throw new Exception(loginStatus.ToString());
            }
            return new EResponse()
            {
                Code = (long)EResponse.RESPONSE_CODE.SUCCESS,
                Message = EResponse.RESPONSE_CODE.SUCCESS.ToString(),
                Data = userToken,
            };
        }
        catch (Exception ex)
        {
            return new EResponse()
            {
                Code = ((long)CConfigAG.CODE_ERROR.IMPLEMEN),
                Message = ex.Message,
                Data = EResponse.RESPONSE_DATA_NULL
            };
        }
    }
    public async Task<EResponse> Api_LoginUserAsync(UserLoginDto? TUM)
    {
        try
        {
            ERequest? requestCheck = await _baseHttpClient.PostAsync<ERequest>(
                _configuration.GetSection(CConfigAG.ENDPOINT_CHECK_USERNAME).Value,
                TUM
            );
            ERequest.LOGIN_CODE loginStatus = (ERequest.LOGIN_CODE)Enum.Parse(
                typeof(ERequest.LOGIN_CODE),
                (requestCheck!.Data ?? "").ToString() ?? ""
            );
            if(loginStatus == ERequest.LOGIN_CODE.NOT_EXIST)
            {
                throw new Exception(loginStatus.ToString());
            }
            ERequest<UserModel>? request = await _baseHttpClient.PostAsync<ERequest<UserModel>>(
                _configuration.GetSection(CConfigAG.ENDPOINT_LOGIN).Value,
                TUM
            );
            string token = await BaseAuthentication.GenerateToken(
                _configuration,
                request!.Data!.Id!,
                request.Data.Username ?? ""
            );

            return new EResponse()
            {
                Code = (long)EResponse.RESPONSE_CODE.SUCCESS,
                Message = EResponse.RESPONSE_CODE.SUCCESS.ToString(),
                Data = new
                {
                    Token = token,
                    ValidTo = BaseAuthentication.GetValidTo(token)
                },
            };
        }
        catch (Exception ex)
        {
            return new EResponse()
            {
                Code = ((long)CConfigAG.CODE_ERROR.IMPLEMEN),
                Message = ex.Message,
                Data = EResponse.RESPONSE_DATA_NULL
            };
        }
    }
    public async Task<EResponse> Api_RegisterUserAsync(UserRegisterDto? TUM)
    {
        try
        {
            ERequest? request = await _baseHttpClient.PostAsync<ERequest>(
                _configuration.GetSection(CConfigAG.ENDPOINT_REGISTER).Value,
                TUM
            );

            return new EResponse()
            {
                Code = (long)EResponse.RESPONSE_CODE.SUCCESS,
                Message = EResponse.RESPONSE_CODE.SUCCESS.ToString(),
                Data = request!.Data
            };
        }
        catch (Exception ex)
        {
            return new EResponse()
            {
                Code = (long)CConfigAG.CODE_ERROR.IMPLEMEN,
                Message = ex.Message,
                Data = EResponse.RESPONSE_DATA_NULL
            };
        }
    }
}