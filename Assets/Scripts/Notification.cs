using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public enum Notification
    {
        RegistrationChosen, // Пользователь выбрал регистрацию (между регистрацией и авторизацией)
        AuthorisationChosen, // Пользователь выбрал авторизацией (между регистрацией и авторизацией)
        RegistrationSend, // Пользователь отправляет данные для регистрации
        AuthorisationSend, // Пользователь отправляет данные для авторизации
        
        BaseAlphaStart, // Пользователь выбрал процедуру измерения базового альфа-ритма
        MatchingStart, // Пользователь выбрал процедуру соотнесения показателей
        GameStart,
        MatchingFinish,
        CloseError
    }


}
