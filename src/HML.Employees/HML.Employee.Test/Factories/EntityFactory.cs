using System;

namespace HML.Employee.Test.Factories
{
	public abstract class EntityFactory<T, U> where T : EntityFactory<T, U>, new() where U : new()
	{
		private readonly U _entity = new U();

		public static T New => new T();

		protected U Entity => this._entity;

		public T With(Action<U> expression)
		{
			expression(this._entity);

			return (T)this;
		}

		protected virtual void DefaultEntity()
		{
		}

		public U Build()
		{
			return this._entity;
		}
	}
}