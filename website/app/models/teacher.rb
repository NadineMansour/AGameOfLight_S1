class Teacher < ActiveRecord::Base
  # Include default devise modules. Others available are:
  # :confirmable, :lockable, :timeoutable and :omniauthable
  devise :database_authenticatable, :registerable,
         :recoverable, :rememberable, :trackable, :validatable
 before_save { |teacher| teacher.school.downcase! }
  has_many :teacher_request_subjects
  has_many :questions
  has_many :teachersubjects
  has_many :subjects, through: :teacher_request_subjects
end
