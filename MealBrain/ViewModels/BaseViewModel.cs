using System.Linq.Expressions;
using System.Reflection;

/*
 * This class is the base view model that other view models should inherit from.
 * The BindableObject parent class provides functionality and handles the INotifyPropertyChanged interface.
 * This should mostly be used for notifying that a property was set in the view model, which will
 * then notify the view that the bound property was changed so that it updates its information.
 */
namespace MealBrain.ViewModels
{
    /// <summary>
    /// This class provides a base for the view models so that we don't
    /// have to define the RaisePropertyChanged method in each view model.
    /// To use this just extend the BaseViewModel from your view model class
    /// and then whenever you change a property value call the RaisePropertyChanged
    /// method at the end of the success path.
    /// </summary>
    public abstract class BaseViewModel : BindableObject
    {
        /// <summary>
        /// This version of the method is in case a function is passed.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>
        public void RaisePropetyChanged<T>(Expression<Func<T>> property)
        {
            var name = GetMemberInfo(property).Name;
            OnPropertyChanged(name);
        }

        /// <summary>
        /// This version of the method is in case the property name directly is passed
        /// to raise the notification. Likely will need to pass in the property name with nameof(propertyname) when calling this.
        /// </summary>
        /// <param name="propertyName"></param>
        public void RaisePropertyChanged(String propertyName)
        {
            OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// This overload parses out the body of a lambda expression to get the member info.
        /// </summary>
        /// <param name="expression">The lambda expression passed as argument, i.e. () => propertyName</param>
        /// <returns>MemberInfo from the expression</returns>
        /// <exception cref="ArgumentException"></exception>
        private MemberInfo GetMemberInfo(LambdaExpression expression)
        {
            MemberExpression? memEx = expression.Body as MemberExpression;

			if (memEx is not null)
				return memEx.Member;
			else
				throw new ArgumentException("Unable to parse expression for Member information", nameof(expression));
		}
    }
}
