namespace ABClient
{
    internal enum AutoboiState
    {
        /// <summary>
        /// Все отключено.
        /// </summary>
        AutoboiOff,
        
        /// <summary>
        /// Автобой, нанесение ударов.
        /// </summary>
        AutoboiOn,
        
        /// <summary>
        /// Восстановление перед кнопкой "завершить".
        /// </summary>
        Restoring,
        
        /// <summary>
        /// Ожидание таймаута боя.
        /// </summary>
        Timeout,
        
        /// <summary>
        /// Вычисление цифр.
        /// </summary>
        Guamod
    }
}
